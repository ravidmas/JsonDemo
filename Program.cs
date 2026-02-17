using System.Drawing;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

var app = builder.Build();

List<Chord> chords = new()
{
    // === Easy ===
    new Chord("C",        "easy",   new[] { new Point(1,3), new Point(2,2), new Point(4,1) }),
    new Chord("A",        "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,2) }),
    new Chord("G",        "easy",   new[] { new Point(0,3), new Point(1,2), new Point(5,3) }),
    new Chord("E",        "easy",   new[] { new Point(1,2), new Point(2,2), new Point(3,1) }),
    new Chord("D",        "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,2) }),
    new Chord("Am",       "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,1) }),
    new Chord("Em",       "easy",   new[] { new Point(1,2), new Point(2,2) }),
    new Chord("Dm",       "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,1) }),
    new Chord("A7",       "easy",   new[] { new Point(2,2), new Point(4,2) }),
    new Chord("E7",       "easy",   new[] { new Point(1,2), new Point(3,1) }),
    new Chord("D7",       "easy",   new[] { new Point(3,2), new Point(4,1), new Point(5,2) }),
    new Chord("Cadd9",    "easy",   new[] { new Point(1,3), new Point(2,2), new Point(4,3), new Point(5,3) }),
    new Chord("Asus2",    "easy",   new[] { new Point(2,2), new Point(3,2) }),
    new Chord("Asus4",    "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,3) }),
    new Chord("Dsus2",    "easy",   new[] { new Point(3,2), new Point(4,3) }),
    new Chord("Dsus4",    "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,3) }),
    new Chord("Esus4",    "easy",   new[] { new Point(1,2), new Point(2,2), new Point(3,2) }),
    new Chord("G6",       "easy",   new[] { new Point(0,3), new Point(1,2) }),
    new Chord("Em7",      "easy",   new[] { new Point(1,2), new Point(2,2), new Point(4,3) }),

    // === Medium ===
    new Chord("Fmaj7",    "medium", new[] { new Point(2,3), new Point(3,2), new Point(4,1) }),
    new Chord("Cmaj7",    "medium", new[] { new Point(1,3), new Point(2,2) }),
    new Chord("G7",       "medium", new[] { new Point(0,3), new Point(1,2), new Point(5,1) }),
    new Chord("Amaj7",    "medium", new[] { new Point(2,2), new Point(3,1), new Point(4,2) }),
    new Chord("Emaj7",    "medium", new[] { new Point(1,2), new Point(2,1), new Point(3,1) }),
    new Chord("Am7",      "medium", new[] { new Point(2,2), new Point(4,1) }),
    new Chord("B7",       "medium", new[] { new Point(1,2), new Point(2,1), new Point(3,2), new Point(5,2) }),
    new Chord("D/F#",     "medium", new[] { new Point(0,2), new Point(3,2), new Point(4,3), new Point(5,2) }),
    new Chord("Gsus4",    "medium", new[] { new Point(0,3), new Point(1,2), new Point(4,1), new Point(5,3) }),

    // === Hard ===
    new Chord("F",        "hard",   new[] { new Point(0,1), new Point(1,3), new Point(2,3), new Point(3,2), new Point(4,1), new Point(5,1) }),
    new Chord("Bm",       "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,4), new Point(4,3), new Point(5,2) }),
    new Chord("Bm7",      "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,2), new Point(4,3), new Point(5,2) }),
    new Chord("F#m",      "hard",   new[] { new Point(0,2), new Point(1,4), new Point(2,4), new Point(3,2), new Point(4,2), new Point(5,2) }),
    new Chord("C#m",      "hard",   new[] { new Point(1,4), new Point(2,6), new Point(3,6), new Point(4,5), new Point(5,4) }),
    new Chord("G#m",      "hard",   new[] { new Point(0,4), new Point(1,6), new Point(2,6), new Point(3,4), new Point(4,4), new Point(5,4) }),
    new Chord("Bb",       "hard",   new[] { new Point(1,1), new Point(2,3), new Point(3,3), new Point(4,3), new Point(5,1) }),
    new Chord("Eb",       "hard",   new[] { new Point(1,6), new Point(2,8), new Point(3,8), new Point(4,8), new Point(5,6) }),
    new Chord("Ab",       "hard",   new[] { new Point(0,4), new Point(1,6), new Point(2,6), new Point(3,5), new Point(4,4), new Point(5,4) }),
    new Chord("Fm",       "hard",   new[] { new Point(0,1), new Point(1,3), new Point(2,3), new Point(3,1), new Point(4,1), new Point(5,1) }),
    new Chord("Cm",       "hard",   new[] { new Point(1,3), new Point(2,5), new Point(3,5), new Point(4,4), new Point(5,3) }),
    new Chord("B",        "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,4), new Point(4,4), new Point(5,2) }),
};

app.MapGet("/getChords", (string? difficulty, int? count) =>
{
    if (difficulty == "all") return Results.Ok(chords);

    var chordList = chords.Where(c =>
        string.IsNullOrEmpty(difficulty) || c.Difficulty == difficulty).ToList();

    if (count != null)
    {
        var rnd = new Random(Guid.NewGuid().GetHashCode());
        while (chordList.Count > count)
            chordList.RemoveAt(rnd.Next(chordList.Count));
    }

    return Results.Ok(chordList);
})
.WithName("GetChords");

app.Run();

class Chord
{
    public string ImgLink { get; set; }
    public string SoundLink { get; set; }
    public string Difficulty { get; set; }
    public string Name { get; set; }
    public Point[] chordPoints { get; set; }

    public Chord(string Name, string Difficulty, Point[] chordPoints)
    {
        this.ImgLink = "";
        this.SoundLink = "";
        this.Difficulty = Difficulty;
        this.Name = Name;
        this.chordPoints = chordPoints;
    }
}