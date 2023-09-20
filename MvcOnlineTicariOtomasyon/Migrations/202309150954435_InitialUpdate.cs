namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.mesajlar2s",
                c => new
                {
                    MesajID = c.Int(nullable: false, identity: true),
                    Gonderici = c.String(maxLength: 50, unicode: false),
                    Alici = c.String(maxLength: 50, unicode: false),
                    Konu = c.String(maxLength: 50, unicode: false),
                    icerik = c.String(maxLength: 2000, unicode: false),
                    Tarih = c.DateTime(nullable: false, storeType: "smalldatetime"),
                })
                .PrimaryKey(t => t.MesajID);
        }
        
        public override void Down()
        {
            DropTable("dbo.mesajlar2s");
        }
    }
}
