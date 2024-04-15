using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoingeckoDb.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("m240215_195800_CreateUnitTable")]
public class m240408_152900_CreateCoinTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Coin",
            columns: t => new
            {
                Id = t.Column<int>(type: "serial"),
                CoinId = t.Column<string>(),
                Price = t.Column<decimal>(),
                Currency = t.Column<string>(),
                DateTime = t.Column<DateTimeOffset>()
            }
        ).PrimaryKey("PK-Coin", c => c.Id);
    }
}