using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using Orchard.Projections.Models;

namespace Orchard.Projections.Handlers {
    public class ProjectionPartHandler : ContentHandler {
        public ProjectionPartHandler(IRepository<ProjectionPartRecord> pagingRepository) {
            Filters.Add(StorageFilter.For(pagingRepository));
        }
    }
}