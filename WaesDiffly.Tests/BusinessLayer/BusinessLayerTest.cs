using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaesDiffly.DataLayer;
using WaesDiffly.CBL;
using WaesDiffly.Tests.MoqObjects;
using WaesDiffly.Constraints;
using WaesDiffly.Model.Constants;
using System.Collections.Generic;
using WaesDiffly.Model.Models;

namespace WaesDiffly.Tests.BusinessLayer
{
    [TestClass]
    public class BusinessLayerTest
    {
        public readonly DiffBL diffBL = new DiffBL(new DataLayerTest().mockDiffRepository);


        /// <summary>
        /// 
        ///                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
        /// </summary>

        [TestMethod]
        public void AddNewDiffRecord_check_Response_isSuccess()
        {
            var response = diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Left, "DQ=="));
            Assert.IsNotNull(response);// Test not null
            Assert.AreEqual(response.ReturnCode, RetCode.Success);
        }


        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// We expect Format exception if given string is not base64 value. It is not problem in code because this exceptions are caught in custom exception filter
        /// </summary>

        [TestMethod]
        public void FindDiff_Check_if_values_equal()
        {
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Left, "DQ=="));
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Right, "DQ=="));
            var response = diffBL.CompareSides(1);
            Assert.IsNotNull(response);// Test not null
            Assert.AreEqual(response.ReturnCode, RetCode.Success);
            Assert.IsNotNull(response.Message);
            Assert.AreEqual(response.Message.Status, InfoText.Equal);
        }

        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// In this control we check size are not equal
        /// </summary>
        [TestMethod]
        public void FindDiff_Check_if_values_are_not_equal_size()
        {
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Left, "DQ=="));
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Right, "GfqqqpU="));
            var response = diffBL.CompareSides(1);
            Assert.IsNotNull(response);// Test not null
            Assert.AreEqual(response.ReturnCode, RetCode.Success);
            Assert.IsNotNull(response.Message);
            Assert.AreEqual(response.Message.Status, InfoText.NotEqualSize);
        }

        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// In this control we check  are not equal content
        /// </summary>
        [TestMethod]
        public void FindDiff_Check_if_values_are_not_equal_content()
        {
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Left, "GbtWiqo="));
            diffBL.AddOrUpdate(1, new DiffRecord(WaesDifflyModel.Models.DiffSide.Right, "GDdWiq4="));
            var response = diffBL.CompareSides(1);

            var expectedResultArray = new List<DiffResult>()
            {
                new DiffResult(){Offset = 0, Length = 2},
                new DiffResult(){Offset = 4, Length = 1},
            };
            Assert.IsNotNull(response);// Test not null
            Assert.AreEqual(response.ReturnCode, RetCode.Success);
            Assert.IsNotNull(response.Message);
            Assert.AreEqual(response.Message.Status, InfoText.NotEqualContent);
            CollectionAssert.AreEqual(response.Message.DiffResultList, expectedResultArray, new DiffResultComparer());
        }
        private class DiffResultComparer : Comparer<DiffResult>
        {
            public override int Compare(DiffResult x, DiffResult y)
            {

                return (x.Length.CompareTo(y.Length) == 0 &&  y.Offset.CompareTo(x.Offset) == 0) ? 0 : -1;
            }
        }

    }
}
