using Microsoft.Extensions.Caching.Memory;
using lab3_School.Models;

namespace lab3_School.CachedModels
{
    public class CachedTeacher
    {
        private readonly SchoolContext _context;
        private readonly IMemoryCache _cache;
        private double time = 2 * 8 + 240;
        public CachedTeacher(SchoolContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public void AddTeachers(string cacheKey, int rowNumber)
        {
            IEnumerable<Teacher> storages = _context.Teachers.Take(20).ToList();
            if (storages != null)
            {
                _cache.Set(cacheKey, storages, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(time)
                });
            }
        }

        public IEnumerable<Teacher> GetTeachers(int rowNumber)
        {
            return _context.Teachers.Take(rowNumber).ToList();
        }

        public IEnumerable<Teacher> GetTeachers(string cacheKey, int rowNumber)
        {
            IEnumerable<Teacher> storages;
            if (!_cache.TryGetValue(cacheKey, out storages))
            {
                storages = _context.Teachers.Take(rowNumber).ToList();
                if (storages != null)
                {
                    _cache.Set(cacheKey, storages, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(time)));
                }
            }
            return storages;
        }

        public string GetTable(IEnumerable<Teacher> teachers)
        {
            string HtmlString = "<html><head><title>Учителя</title>" +
                "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/></head><body>" +
                        "<a href = '/'>На главную</a><br>" +
                        "<p>Список учителей:</p>" +
                        "<table border=1>" +
                        "<tr>" +
                        "<th>Id</th>" +
                        "<th>Имя</th>"+
                        "<th>Фамилия</th>" +
                        "<th>Отчество</th>" +
                        "<th>Должность</th>" +
                        "</tr>";
            foreach (var teacher in teachers)
            {
                HtmlString += "<tr>" +
                $"<td>{teacher.Id}</td>" +
                $"<td>{teacher.Name}</td>"+
                $"<td>{teacher.Surname}</td>" +
                $"<td>{teacher.MiddleName}</td>" +
                $"<td>{teacher.Position}</td>" +
                "</tr>";
            }
            HtmlString += "</table></body></html>";
            return HtmlString;
        }

        public string GetTable(string name)
        {
            var teachers = _context.Teachers.ToList()
                .Where(x => x.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
            return GetTable(teachers);
        }
    }
}
