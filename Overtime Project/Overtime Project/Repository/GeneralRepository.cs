using Microsoft.EntityFrameworkCore;
using Overtime_Project.Context;
using Overtime_Project.Repository.Interface;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Overtime_Project.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : OvertimeContext
    {
        private readonly OvertimeContext conn;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(OvertimeContext conn)
        {
            this.conn = conn;
            entities = conn.Set<Entity>();
        }
        public int Delete(Key key)
        {
            var delEntity = entities.Find(key);
            entities.Remove(delEntity);
            var result = conn.SaveChanges();
            return result;
        }
        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }
        public Entity Get(Key key)
        {
            return entities.Find(key);
        }
        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = conn.SaveChanges();
            return result;
        }
        public int Update(Entity entity)
        {
            conn.Entry(entity).State = EntityState.Modified;
            var result = conn.SaveChanges();
            return result;
        }
    }
}
