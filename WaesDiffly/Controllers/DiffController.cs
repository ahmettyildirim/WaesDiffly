using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WaesDiffly.CBL;
using WaesDiffly.Filters;
using WaesDiffly.Model.Constants;
using WaesDiffly.Model.Models;
using WaesDiffly.Models;
using WaesDifflyModel.Models;

namespace WaesDiffly.Controllers
{   /// <summary>
    /// This is a main controller. Provides, get and post methods for getting difference between two base64 encoded strings.
    /// </summary>
    [CustomException] // this is a custom exception filter.
    [ModelValidation] // This is generic model validation filter. For now, it controls if given string is base64encoded or not.
    [RoutePrefix("v1/diff")] 
    public class DiffController : ApiController
    {
        private readonly DiffBL diffBL = new DiffBL(); // This is a constructor for busines logic operations. All operations are done in CBL layer.

        public DiffController()
        {

        }

        public DiffController(DiffBL diffBL)
        {
            this.diffBL = diffBL;
        }
        


        /// <summary>
        /// Returns the result of diffying for giving ID.
        /// </summary>
        /// <param name="id">Defines Id of left and right side that wants to diff</param>
        /// <returns>The diff result or error message</returns>
        [Route("{id:int:min(1)}")]
        [ResponseType(typeof(DiffResponse))]
        public IHttpActionResult Get(int id)
        {
            var result = diffBL.CompareSides(id);
            if (result.ReturnCode != RetCode.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Message);
        }
        /// <summary>
        /// Create or updates new side of given Id
        /// </summary>
        /// <param name="id">Defines Id of left and right side that wants to diff</param>
        /// <param name="side">Left or Right</param>
        /// <param name="data">json object. Should contains an element named "Content" which should be base64Data.</param>
        /// <returns>If succeed return 201 code, otherwise returns error message</returns>
        [Route("{id:int:min(1)}/{side:diffside}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Post(int id, DiffSide side, [FromBody]Base64Data data)
        {
            var result = diffBL.AddOrUpdate(id, new DiffRecord(side, data.Content));
            if (result.ReturnCode != RetCode.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

    }


}
