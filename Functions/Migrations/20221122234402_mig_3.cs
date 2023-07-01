using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                CREATE FUNCTION bestSellingStaff(@totalOrderPrice INT = 10000)
	                RETURNS TABLE //inline function olduğu için gerye bir table dönecek o sebeple return table dedi.
                AS
                RETURN 
                SELECT TOP 1 p.Name, COUNT(*) OrderCount, SUM(o.Price) TotalOrderPrice FROM Persons p
                JOIN Orders o
	                ON p.PersonId = o.PersonId
                GROUP By p.Name
                HAVING SUM(o.Price) < @totalOrderPrice
                ORDER By OrderCount DESC
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP FUNCTION bestSellingStaff");
        }
    }
}
