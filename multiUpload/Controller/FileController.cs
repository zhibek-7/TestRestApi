using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using multiUpload.Models;

namespace multiUpload.Controller
{
    [Route("api/file")]
   public class FileController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Uploads([FromForm] List<IFormFile> files) {

            try {
                var result = new List<FileUploadResult>();
                foreach (var file in files) {
                  var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images",file.FileName);
                  var stream = new FileStream(path,FileMode.Create);
                  file.CopyToAsync(stream);
                    result.Add(new FileUploadResult() {Name=file.FileName, Length=file.Length });
                }
                return Ok(result);
            
            } 
            catch {
                return BadRequest();
            }

        }
    }
}