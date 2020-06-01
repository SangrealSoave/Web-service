using Microsoft.EntityFrameworkCore.Migrations;

namespace MscJobsForDisabled.WebService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    WorkingHours = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Email", "Location", "Name", "Telephone", "WorkingHours" },
                values: new object[] { 1L, "arina.donich@nami.ru  ", "город Москва, Автомоторная улица, дом 2", "Инженер-программист", "(495) 456-43-51", "пятидневная рабочая неделя " });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Email", "Location", "Name", "Telephone", "WorkingHours" },
                values: new object[] { 2L, "arina.donich@nami.ru  ", "город Москва, Автомоторная улица, дом 2", "Инженер-конструктор", "(495) 456-43-51", "пятидневная рабочая неделя " });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Email", "Location", "Name", "Telephone", "WorkingHours" },
                values: new object[] { 3L, "tishumilina@rolf.ru", "город Москва, 39-й километр Московской Кольцевой Автодороги, владение 1, строение 1", "Продавец-консультант", "(495) 777-77-15 доб. 30171", "график сменности  " });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Email", "Location", "Name", "Telephone", "WorkingHours" },
                values: new object[] { 4L, "MGorbacheva@nestro.ru ", "город Москва, Армянский переулок, дом 9, строение 1 ", "Переводчик", "(495) 748-64-24 доб. 1008 ", "с неполным рабочим днем" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
