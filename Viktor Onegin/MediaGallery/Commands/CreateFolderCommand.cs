using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaGallery.Models;

namespace MediaGallery.Commands
{
    public class CreateFolderCommand : CompositeCommand<EditFolderModel>
    {
        public CreateFolderCommand(CreateFolderToStoreCommand cfToStoreCommand,
                                   CreateFolderToDatabaseCommand cfToDbCommand)
        {
            Add(cfToStoreCommand);
            Add(cfToDbCommand);
        }
    }
}
