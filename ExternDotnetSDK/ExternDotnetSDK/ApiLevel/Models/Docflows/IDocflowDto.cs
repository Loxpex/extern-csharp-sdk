using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    public interface IDocflowDto
    {
        public Urn Type { get; }
        public DocflowDescription Description { get; set; }
    }
}