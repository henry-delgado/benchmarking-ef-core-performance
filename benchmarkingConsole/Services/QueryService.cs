using BenchmarkDotNet.Attributes;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace benchmarkingConsole.Services
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class QueryService
    {
        private readonly BadApplicationDbContext _context;

        [Params(500000)]
        public int TotalRecords;

        [Params(3)]
        public int TotalProjectsPerCompany;

        [GlobalSetup]
        public void Setup()
        {
            _context.Database.Migrate();
            _context.SeedData(TotalRecords, TotalProjectsPerCompany);            
        }

        public QueryService()
        {
            _context = new BadApplicationDbContext();
        }

        [Benchmark]
        [BenchmarkCategory("Penetration Attributes")]
        public void GetRawPenetrationAttributesWithJoins()
        {
            _context.BadPenetrationAttribute.FromSqlRaw(@"
            SELECT p.* FROM ​
            ( ​

                SELECT ​

                    PA.Id, ​

                    PA.[NAME], ​

                    PA.[TYPE], ​

                    PA.SELECTEDVALUE, ​

                    PA.BADPROJECTID , ​

                    PA.BADPENETRATIONID, ​

                    PA.ISEDITABLE, ​

                    ISNULL(SP.[PRIORITY], PA.[PRIORITY]) AS [PRIORITY], ​

                    PA.CATEGORYID, ​

                    SP.ID STD_ATTRIBUTE_ID ​

                FROM ​

                    BADPENETRATIONATTRIBUTE PA ​

                    INNER JOIN ​

                        BADPENETRATION PEN ​

                        ON PEN.ID = PA.BADPENETRATIONID ​

                    LEFT OUTER JOIN ​

                        BADSTANDARDPROJECTPENETRATION SP ​

                        ON SP.BADPROJECTID = PEN.BADPROJECTID ​

                        AND SP.ISDELETED = 'N' ​

                        AND SP.CATEGORYID = PEN.CATEGORYID ​

                        AND SP.[NAME] = PA.[NAME] ​

            ) ​
            P ​");
        }

        [Benchmark]
        [BenchmarkCategory("Penetration Attributes")]
        public void GetRawPenetrationAttributesWithoutStandardProjectJoin()
        {
            _context.BadPenetrationAttribute.FromSqlRaw(@"
            SELECT  
                pa.id,
                pa.[NAME],
                pa.[TYPE],
                pa.selectedvalue,
                pen.badprojectid,
                pa.badpenetrationid,
                pa.iseditable,
                pa.[PRIORITY] as [PRIORITY],
                ​pa.categoryid
            FROM
                badpenetrationattribute pa ​
            INNER JOIN ​ 
                badpenetration pen ​
            ON
                pen.id = pa.badpenetrationid
            ");
        }

        [Benchmark]
        [BenchmarkCategory("Penetration Attributes")]
        public void GetRawPenetrationAttributes()
        {
            _context.BadPenetrationAttribute.FromSqlRaw(@"
            SELECT  
                pa.id,
                pa.[NAME],
                pa.[TYPE],
                pa.selectedvalue,
                pa.badpenetrationid,
                pa.iseditable,
                pa.[PRIORITY] as [PRIORITY],
                ​pa.categoryid
            FROM
                badpenetrationattribute pa            
            ");
        }        
    }
}