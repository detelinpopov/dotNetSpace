using System;
using Interfaces.Sql.Entities;

namespace Sql.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}