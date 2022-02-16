using System.ComponentModel.DataAnnotations;

namespace Git.ViewModels.Repositories
{
    public class CreateRepositoryModelView
    {
        public string Name { get; set; }
        public string RepositoryType { get; set; }
    }
}
