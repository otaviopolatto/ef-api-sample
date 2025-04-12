using Financecontrol.Infrastructure.Persistence;
using FinanceControl.Domain.Dtos.Lancamentos;
using FinanceControl.Domain.Dtos;
using FinanceControl.Domain.Dtos.Tag;
using FinanceControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static FinanceControl.Domain.Dtos.Tag.TagResponseDto;

namespace FinanceControl.Service.TagService
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<TagResponse>> CreateTag(CreateTagRequest request)
        {

            // var tag = _mapper.Map<Tag>(request);

            Tag tag = (Tag) request;

            _context.Tags.Add(tag);

            var saveResponse = await _context.SaveChangesAsync();

            if(saveResponse > 0)
            {
                await _context.Entry(tag).ReloadAsync();

                var tagResponse = new TagResponse(tag.Id, tag.Descricao);

                return new Response<TagResponse> { Status = 201, Message = "Tag registrada com sucesso", Data = tagResponse, IsSuccess = true };
          
            }

            return new Response<TagResponse> { Status = 400, Message = "Falha ao registrar tag", IsSuccess = false };

        }

        public async Task<Response<TagResponse>> GetTagById(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag != null)
            {
                var tagResponse = new TagResponse(tag.Id, tag.Descricao);
                return new Response<TagResponse> { IsSuccess = true, Data = tagResponse, Status = 200 };               
            }

            return new Response<TagResponse> { IsSuccess = false, Message = "Tag não encontrada", Status = 404 };

        }

        public async Task<IEnumerable<TagResponse>> GetTags()
        {
            var tagsResponse = await _context.Tags.ToListAsync();

            List<TagResponse> tagResponseList = new List<TagResponse>();

            tagsResponse.ForEach(it =>
            {
                var tagResponse = new TagResponse(it.Id, it.Descricao);
                tagResponseList.Add(tagResponse);
            });

            return tagResponseList;

        }





    }
}
