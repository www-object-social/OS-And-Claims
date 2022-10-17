using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongPing.uniformResource
{
    public class Identifier:StandardInternal.pongPing.uniformResource.IIdentifier
    {
        public string Path { get; private set; }
        public int LimitOfConnection { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Use { get; private set; }
        public Guid ID { get; private set; }
        public int UsedConnections { get; private set; }

        private readonly IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory;
        public Identifier(string Path,int LimitOfConnection, IDbContextFactory<ServerStorages.OSAndClaimsContext> DbContextFactory)
        {
            this.DbContextFactory = DbContextFactory;
            this.Path = Path;
            this.LimitOfConnection = LimitOfConnection;
           
            using var Db = this.DbContextFactory.CreateDbContext();
            ServerStorages.PongPingUniformResourceIdentifier PPURI;
            if (Db.PongPingUniformResourceIdentifiers.Any(x => x.Path == Path)) {

                PPURI = Db.PongPingUniformResourceIdentifiers.Single(x => x.Path == Path);

                goto SetData;
            }
            PPURI = Db.PongPingUniformResourceIdentifiers.Add(new ServerStorages.PongPingUniformResourceIdentifier
            {
                Id = Guid.NewGuid(),
                Created = DateTime.UtcNow,
                Path = this.Path,
                Use = DateTime.UtcNow,
                UsedConnections = 0
            }).Entity;
            Db.SaveChanges();
        SetData:
            this.UsedConnections = PPURI.UsedConnections;
            this.Created = PPURI.Created;
            this.Use = PPURI.Use;
            this.ID = PPURI.Id;
        }   
    }
}
