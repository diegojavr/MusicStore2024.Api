using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SqlReportes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE uspReportSales (@DateStart DATE, @DateEnd DATE)
                as
                BEGIN
                	SELECT 
                		C.Title ConcertName,
                		SUM(S.Total) AS Total
                	FROM Sale S(NOLOCK)
                	INNER JOIN Concert C (NOLOCK) ON S.ConcertId = C.Id
                	AND C.[Status] =1
                	AND S.SaleDate BETWEEN @DateStart AND @DateEnd
                	GROUP BY C.Title
                	ORDER BY 2 DESC


                END
                GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE uspReportSales");
        }
    }
}
