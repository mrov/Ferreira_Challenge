using Models.DTOs.Output;

namespace Utils
{
    public class UserPagination
    {
        public UserPagination(int totalUsers, int totalPages,int? currentPage,int pageSize, List<UserDTO> users) {
            TotalPages = totalPages;
            TotalUsers = totalUsers;
            CurrentPage = (int)currentPage;
            PageSize = pageSize;
            Users = users;
        }

        public int TotalUsers { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
