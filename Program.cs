var filePath = "input.txt";
var inputReports = ReadInput(filePath);
Console.WriteLine("Reports:");
foreach (var report in inputReports)
{
    Console.WriteLine(string.Join(" ", report));
}

var count = CalculateSafeReports(inputReports);
Console.WriteLine($"The total no of safe reports are {count}");

return;

List<List<int>> ReadInput(string path)
{
    return File.ReadAllLines(filePath)
        .Select(line => line.Trim()
            .Split(' ')
            .Select(int.Parse)
            .ToList())
        .ToList();
}

int CalculateSafeReports(List<List<int>> reports)
{
    var safeReports = 0;

    foreach (var report in reports)
    {
        var isSafe = IsSafe(report);
        if (isSafe)
        {
            safeReports++;
        }
        else
        {
            Console.WriteLine($"Not safe {string.Join(", ", report)}");
            for (var i = 0; i < report.Count; i++)
            {
                var modifiedReport = report.Where((val, index) => index != i).ToList();
                if (IsSafe(modifiedReport))
                {
                    Console.WriteLine($"\t Safe removing index {i}");
                    safeReports++;
                    break;
                }
            }
        }
    }
    return safeReports;
}

bool IsSafe(List<int> report)
{
    var isSafe = true;
    if (report.Count < 2) return false;


    for (var i = 1; i < report.Count; i++)
    {
        var diff = Math.Abs(report[i] - report[i - 1]);

        if (diff is < 1 or > 3)
        {
            isSafe = false;
            break;
        }
    }

    var isIncreasing = report.OrderBy(x => x).SequenceEqual(report);
    var isDecreasing = report.OrderByDescending(x => x).SequenceEqual(report);
    isSafe = isIncreasing || isDecreasing;
    return isSafe;
}