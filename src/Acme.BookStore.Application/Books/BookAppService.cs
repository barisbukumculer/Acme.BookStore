using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books
{
    [Authorize(BookStorePermissions.Books)]
    public class BookAppService : ApplicationService, IBookAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IEntityCache<BookDto, Guid> _bookCache;

        public BookAppService(IRepository<Book, Guid> bookRepository, IEntityCache<BookDto,Guid> bookCache)
        {
            _bookRepository = bookRepository;
            _bookCache = bookCache;
        }

        public async Task<BookDto> GetAsync (Guid id)
        {
           return await _bookCache.GetAsync(id);
        } 

        [Authorize(BookStorePermissions.Books_Create)]
        public async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
        {
            var book=ObjectMapper.Map<CreateUpdateBookDto,Book>(input); 
            await _bookRepository.InsertAsync(book);


            if (book.Name == "test")
            {
                throw new UserFriendlyException("Invalid book name: " + book.Name);
            }


            return ObjectMapper.Map<Book, BookDto>(book);   
        }
        [Authorize(BookStorePermissions.Books_Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            List<Book> books = await _bookRepository.GetPagedListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting ?? nameof(Book.Name)
                );
            long totalBookCount = await _bookRepository.GetCountAsync();

            return new PagedResultDto<BookDto>
            {
                TotalCount = totalBookCount,
                Items = ObjectMapper.Map<List<Book>,
                List<BookDto>>(books)
            };
        }
    }
}
