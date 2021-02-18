using InterGalacticEcomm.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1
{
    public class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly GalacticDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _db = new GalacticDbContext(
                new DbContextOptionsBuilder<GalacticDbContext>()
                .UseSqlite(_connection)
                .Options);
            _db.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}
