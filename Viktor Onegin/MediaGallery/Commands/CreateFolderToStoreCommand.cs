using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaGallery.Data;
using MediaGallery.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace MediaGallery.Commands
{
    public class CreateFolderToStoreCommand : ICommand<EditFolderModel>
    {
        private readonly ApplicationDbContext _dataContext;
        private readonly ILogger<CreateFolderToDatabaseCommand> _logger;
        private readonly IHostingEnvironment _host;
        private readonly GalleryContext _galleryContext;

        public CreateFolderToStoreCommand(IHostingEnvironment host,
                                          ILogger<CreateFolderToDatabaseCommand> logger,
                                          ApplicationDbContext dataContext,
                                          GalleryContext galleryContext)
        {
            _galleryContext = galleryContext;
            _host = host;
            _logger = logger;
            _dataContext = dataContext;
        }
        public bool Execute(EditFolderModel model)
        {
            var parentPath = "";
            var folder = new MediaFolder();
            folder.Title = model.Title;

            if (model.parentFolderId.HasValue)
            {
                var parentFolder = _dataContext.Folders.FirstOrDefault(f => f.Id == model.parentFolderId);
                parentPath = _galleryContext.GetFolderPath(model.parentFolderId.Value, null);
            }

            var path = Path.Combine(_host.WebRootPath, "gallery", parentPath, folder.Title);
            Directory.CreateDirectory(path);

            return true;
        }

        public List<string> Validate (EditFolderModel parameter)
        {
            return new List<string>();
        }
        public bool Rollback()
        {
            return true;
        }
    }
}
