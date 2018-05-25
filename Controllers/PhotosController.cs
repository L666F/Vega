using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Models;
using vega.Models.Data;

namespace vega.Controllers
{
    [Route("/api/makes/{makeId}/photos")]
    public class PhotosController : Controller
    {
        private readonly PhotoSettings photoSettings;
        private readonly IHostingEnvironment host;
        private readonly VegaDbContext _context;
        private readonly IMapper mapper;
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        public PhotosController(IHostingEnvironment host, VegaDbContext _context, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.host = host;
            this._context = _context;
            this.mapper = mapper;
        }

        public async Task<IActionResult> UploadAsync(int makeId, IFormFile file)
        {
            var make = _context.Makes.SingleOrDefault(m => m.ID == makeId);
            if (make == null)
                return NotFound();

            if (file == null)
                return BadRequest("Null file");
            if (file.Length == 0)
                return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes)
                return BadRequest("Maximum file size exceeded (10MB)");
            if (!photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid file type (jpg,jpeg,png)");
            



            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            make.Photos.Add(photo);
            await _context.SaveChangesAsync();
            return Ok(mapper.Map<Photo,PhotoResource>(photo));
        }
    }
}