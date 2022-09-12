using FileHost.WebApi.Entities;
using FileHost.WebApi.Helpers;
using FileHost.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileHost.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly FileService? _service;

        public FileController(FileService service) => _service = service;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(string id)
        {
            var response = await _service.GetAsync(id);

            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostFile([FromForm] IFormFile file)
        {
            var entity = new FileData();
            entity.Source = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            byte[]? formFile = null;

            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                formFile = binaryReader.ReadBytes((int)file.Length);
            }

            entity.FileName = file.FileName;
            entity.Format = file.ContentType;
            entity.Data = formFile;
            entity.Id = HashFile.GetHashString(entity.FileName + entity.Source + entity.Format +
               System.Text.Encoding.UTF8.GetString(entity.Data));
            entity.Size = file.Length;
            entity.StorageTime = DateTime.Now.AddDays(5);

            var response = await _service.CreateAsync(entity);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
