using System;

namespace F23.DataAccessExtensions.UnitTests
{
    internal class ShreddableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
