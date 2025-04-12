using FinanceControl.Domain.Dtos;
using FinanceControl.Domain.Dtos.Tag;
using static FinanceControl.Domain.Dtos.Tag.TagResponseDto;

namespace FinanceControl.Service.TagService
{
    public interface ITagService
    {
        Task<Response<TagResponse>> CreateTag(CreateTagRequest request);

        Task<Response<TagResponse>> GetTagById(int id);

        Task<IEnumerable<TagResponse>> GetTags();
    }
}
