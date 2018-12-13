using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WaesDiffly.DataLayer;
using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
using WaesDifflyModel.Models;

namespace WaesDiffly.Tests.MoqObjects
{
    [TestClass]
    public class DataLayerTest
    {
        public readonly AbstractDataLayer mockDiffRepository;

        public DataLayerTest()
        {
            var objList = new List<DiffObj>();

            
            var mockDiffRepository = new Mock<StaticLayer>();
            mockDiffRepository
                .Setup(mr => mr.GetDiffObj(It.IsAny<int>()))
                .Returns((int i) =>
                  objList.FirstOrDefault(x => x.Id == i) == null ?   new ResponseMessage<DiffObj>().RecordNotFoundError() : new ResponseMessage<DiffObj>() { Message = objList.FirstOrDefault(x => x.Id == i) });


            mockDiffRepository.Setup(mr => mr.CreateNewObj(It.IsAny<DiffObj>())).Callback(
               (DiffObj target) =>
               {
                   objList.Clear();
                   objList.Add(target);
               }).Returns(() => new ResponseMessage());

            mockDiffRepository.Setup(mr => mr.UpdateCurrentObj(It.IsAny<int>(), It.IsAny<DiffRecord>())).Callback(
                (int targetId, DiffRecord target) =>
                {
                    var original = objList.Where(q => q.Id == targetId).FirstOrDefault();

                    if (original == null)
                    {
                        throw new InvalidOperationException();
                    }

                    if (target.Side == DiffSide.Left)
                    {
                        original.LeftRecord = target;
                    }
                    else
                    {
                        original.RightRecord = target;
                    }

                }).Returns(() => new ResponseMessage());
            this.mockDiffRepository = mockDiffRepository.Object;
        }


        [TestMethod()]
        public void CreateNewDiff_Test()
        {
            var obj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            var expected = this.mockDiffRepository.CreateNewObj(obj);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.ReturnCode, RetCode.Success);
        }

        [TestMethod()]
        public void GetById_CheckCorrect()
        {
            var leftObj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            var rightObj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            this.mockDiffRepository.CreateNewObj(leftObj);
            this.mockDiffRepository.CreateNewObj(rightObj);

            var expected = this.mockDiffRepository.GetDiffObj(1);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.ReturnCode, RetCode.Success);
            Assert.AreEqual(expected.Message.Id, 1);
        }

        [TestMethod()]
        public void UpdateById_CheckResponse_And_Get_CheckResult()
        {
            var leftObj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            var rightObj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            this.mockDiffRepository.CreateNewObj(leftObj);
            this.mockDiffRepository.CreateNewObj(rightObj);

            var newRecord = new DiffRecord(DiffSide.Left, "DA==");
            var expectedUpdate = this.mockDiffRepository.UpdateCurrentObj(1, newRecord);
            Assert.IsNotNull(expectedUpdate);
            Assert.AreEqual(expectedUpdate.ReturnCode, RetCode.Success);
            

            var expected = this.mockDiffRepository.GetDiffObj(1);
            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.ReturnCode, RetCode.Success);
            Assert.AreEqual(expected.Message.Id, 1);
        }





        [TestMethod()]
        public void AddNewItem()
        {
            var obj = new DiffObj(1, new DiffRecord(DiffSide.Left, "DQ=="));
            AbstractDataLayer  layer = new StaticLayer();
            layer.CreateNewObj(obj);
            var item = layer.GetDiffObj(1);

            var expected = this.mockDiffRepository.CreateNewObj(obj);

            Assert.IsNotNull(expected);
            Assert.AreEqual(expected.ReturnCode, RetCode.Success);

            var expectedRet = this.mockDiffRepository.GetDiffObj(1);
            Assert.IsNotNull(expectedRet);
            Assert.AreEqual(expectedRet.ReturnCode, Model.Constants.RetCode.Success);
            Assert.AreEqual(expectedRet.Message.Id, 1);

            var newRecord = new DiffRecord(DiffSide.Left, "DA==");
            var expectedUpdate = this.mockDiffRepository.UpdateCurrentObj(1, newRecord);
            Assert.IsNotNull(expectedUpdate);
            Assert.AreEqual(expectedUpdate.ReturnCode, RetCode.Success);

        }
    }
}