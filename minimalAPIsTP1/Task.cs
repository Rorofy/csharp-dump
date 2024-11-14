namespace minimalAPIsTP1;

public record Task(int id, string title, DateTime startDate, DateTime? endDate = null);
