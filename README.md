# Benchmarking Performance with EF Core

Testing SQL performance with various EF Core techniques using BenchmarkDotNet for measuring.

## EF Core: Loading Relationships
1. **Eager Loading**
    ```
    var company = context.Companies
        .Include(p=> p.Projects)...First();
    ```
2. **Explicit Loading**
    ```
    context.Entry(company)
        .Collection(p=> p.Projects).Load();
    ```
3. **Lazy Loading**
    ```
    public virtual ICollection<Project> Projects { get; set; }
    ```
4. **Select Loading**
   ```
   var a = context.Companies.Select(p=> new {p.Name, p.Projects, ...}).First();
   ```

## Benchmark Reading the Database

Type of Query | SQL Commands | DB Trips | Time(us)/%
--------------|--------------|----------|------------
Select Loading | 1 | 1 | 750 us 100%
Eager Loading  | 1 | 1 | 1,000 us 133%
Explicit and Lazy Loading | 6 | 6 | 4,500 us 600%

### Notes
The issue is not about how many database commands we have but rather about how many database trips. It will hit the performance.
Lazy loading is easy to use but it makes the database very chatty.

## Performance Tips by [The Reformed Programmer](https://www.thereformedprogrammer.net/)
1. Do not load data you do not need
2. Do not include relationships but pickout what you need from the relationships
3. If possible, move calculations to database
4. Add SQL indexes to any property to sort or filter on
5. Add AsNoTracking method to your query or do not load any entity classes

## REST and DTOs = Pragmatic REST
There many resources for making RESTful APIs. However, I believe that after reviewing the benchmarking results on how we query the database, we should aim for what makes more sense to our case and goals in mind. 
We should not be radical trying to implement RESTful services if that would significantly decrease the performance of the application. On the other side, we should balance our practices to create maintainable, sutainable and scalable code.