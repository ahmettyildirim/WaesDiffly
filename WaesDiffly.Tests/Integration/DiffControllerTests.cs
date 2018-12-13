

namespace WaesDiffly.Test.Integration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Results;
    using WaesDiffly.CBL;
    using WaesDiffly.Controllers;
    using WaesDiffly.Model.Constants;
    using WaesDiffly.Model.Models;
    using WaesDiffly.Models;
    using WaesDiffly.Tests.MoqObjects;
    using WaesDifflyModel.Models;

    [TestClass()]
    public class DiffControllerTests
    {
        public readonly DiffController diffController = new DiffController(new DiffBL(new DataLayerTest().mockDiffRepository));


        [TestMethod]
        public void AddNewDiffRecord_check_Response_isSuccess()
        {
            var response = diffController.Post(1, DiffSide.Left, new Base64Data() { Content = "DQ==" });
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }


        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// We expect Format exception if given string is not base64 value. It is not problem in code because this exceptions are caught in custom exception filter
        /// </summary>

        [TestMethod]
        public void FindDiff_Check_if_values_equal()
        {
            diffController.Post(1, DiffSide.Left, new Base64Data() { Content = "DQ==" });
            diffController.Post(1, DiffSide.Right, new Base64Data() { Content = "DQ==" });
            var response = diffController.Get(1);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<DiffResponse>));
            Assert.AreEqual(((OkNegotiatedContentResult<DiffResponse>) response).Content.Status, InfoText.Equal);
        }

        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// In this control we check size are not equal
        /// </summary>
        [TestMethod]
        public void FindDiff_Check_if_values_are_not_equal_size()
        {
            diffController.Post(1, DiffSide.Left, new Base64Data() { Content = "DQ==" });
            diffController.Post(1, DiffSide.Right, new Base64Data() { Content = "GfqqqpU=" });
            var response = diffController.Get(1);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<DiffResponse>));
            Assert.AreEqual(((OkNegotiatedContentResult<DiffResponse>)response).Content.Status, InfoText.NotEqualSize);

        }

        /// <summary>
        /// This test controls difference with Id. For the unit test I use moq object layer in DataLayerTest.
        /// In this control we check  are not equal content
        /// </summary>
        [TestMethod]
        public void FindDiff_Check_if_values_are_not_equal_content()
        {
            diffController.Post(1, DiffSide.Left, new Base64Data() { Content = "GbtWiqo=" });
            diffController.Post(1, DiffSide.Right, new Base64Data() { Content = "GDdWiq4=" });
            var response = diffController.Get(1);

            var expectedResultArray = new List<DiffResult>()
            {
                new DiffResult(){Offset = 0, Length = 2},
                new DiffResult(){Offset = 4, Length = 1},
            };
            Assert.IsNotNull(response);// Test not null
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkNegotiatedContentResult<DiffResponse>));
            Assert.AreEqual(((OkNegotiatedContentResult<DiffResponse>)response).Content.Status, InfoText.NotEqualContent);
            CollectionAssert.AreEqual(((OkNegotiatedContentResult<DiffResponse>)response).Content.DiffResultList, expectedResultArray, new DiffResultComparer());
        }
        private class DiffResultComparer : Comparer<DiffResult>
        {
            public override int Compare(DiffResult x, DiffResult y)
            {

                return (x.Length.CompareTo(y.Length) == 0 && y.Offset.CompareTo(x.Offset) == 0) ? 0 : -1;
            }
        }



    }
}