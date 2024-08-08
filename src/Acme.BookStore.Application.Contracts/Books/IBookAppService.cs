using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService : IApplicationService
    {
        Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<BookDto> CreateAsync(CreateUpdateBookDto input);
        Task DeleteAsync(Guid id);  
    }
}
