using System.Drawing;
using System.Globalization;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


List<Chord> chords = new()
{
    // === Easy ===
    new Chord("C",        "easy",   new[] { new Point(1,3), new Point(2,2), new Point(4,1) }),           // x32010
    new Chord("A",        "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,2) }),           // x02220
    new Chord("G",        "easy",   new[] { new Point(0,3), new Point(1,2), new Point(5,3) }),           // 320003
    new Chord("E",        "easy",   new[] { new Point(1,2), new Point(2,2), new Point(3,1) }),           // 022100
    new Chord("D",        "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,2) }),           // xx0232
    new Chord("Am",       "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,1) }),           // x02210
    new Chord("Em",       "easy",   new[] { new Point(1,2), new Point(2,2) }),                           // 022000
    new Chord("Dm",       "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,1) }),           // xx0231
    new Chord("A7",       "easy",   new[] { new Point(2,2), new Point(4,2) }),                           // x02020
    new Chord("E7",       "easy",   new[] { new Point(1,2), new Point(3,1) }),                           // 020100
    new Chord("D7",       "easy",   new[] { new Point(3,2), new Point(4,1), new Point(5,2) }),           // xx0212
    new Chord("Cadd9",    "easy",   new[] { new Point(1,3), new Point(2,2), new Point(4,3), new Point(5,3) }), // x32033
    new Chord("Asus2",    "easy",   new[] { new Point(2,2), new Point(3,2) }),                           // x02200
    new Chord("Asus4",    "easy",   new[] { new Point(2,2), new Point(3,2), new Point(4,3) }),           // x02230
    new Chord("Dsus2",    "easy",   new[] { new Point(3,2), new Point(4,3) }),                           // xx0230 (fretted G2,B3)
    new Chord("Dsus4",    "easy",   new[] { new Point(3,2), new Point(4,3), new Point(5,3) }),           // xx0233
    new Chord("Esus4",    "easy",   new[] { new Point(1,2), new Point(2,2), new Point(3,2) }),           // 022200
    new Chord("G6",       "easy",   new[] { new Point(0,3), new Point(1,2) }),                           // 320000
    new Chord("Em7",      "easy",   new[] { new Point(1,2), new Point(2,2), new Point(4,3) }),           // 022030

    // === Medium ===
    new Chord("Fmaj7",    "medium", new[] { new Point(2,3), new Point(3,2), new Point(4,1) }),           // xx3210
    new Chord("Cmaj7",    "medium", new[] { new Point(1,3), new Point(2,2) }),                           // x32000
    new Chord("G7",       "medium", new[] { new Point(0,3), new Point(1,2), new Point(5,1) }),           // 320001
    new Chord("Amaj7",    "medium", new[] { new Point(2,2), new Point(3,1), new Point(4,2) }),           // x02120
    new Chord("Emaj7",    "medium", new[] { new Point(1,2), new Point(2,1), new Point(3,1) }),           // 021100
    new Chord("Am7",      "medium", new[] { new Point(2,2), new Point(4,1) }),                           // x02010
    new Chord("B7",       "medium", new[] { new Point(1,2), new Point(2,1), new Point(3,2), new Point(5,2) }), // x21202
    new Chord("D/F#",     "medium", new[] { new Point(0,2), new Point(3,2), new Point(4,3), new Point(5,2) }), // 2x0232
    new Chord("Gsus4",    "medium", new[] { new Point(0,3), new Point(1,2), new Point(4,1), new Point(5,3) }),  // 3 2 0 0 1 3

    // === Hard (barre and stretches) ===
    new Chord("F",        "hard",   new[] { new Point(0,1), new Point(1,3), new Point(2,3), new Point(3,2), new Point(4,1), new Point(5,1) }), // 133211
    new Chord("Bm",       "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,4), new Point(4,3), new Point(5,2) }),                 // x24432
    new Chord("Bm7",      "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,2), new Point(4,3), new Point(5,2) }),                 // x24232
    new Chord("F#m",      "hard",   new[] { new Point(0,2), new Point(1,4), new Point(2,4), new Point(3,2), new Point(4,2), new Point(5,2) }), // 244222
    new Chord("C#m",      "hard",   new[] { new Point(1,4), new Point(2,6), new Point(3,6), new Point(4,5), new Point(5,4) }),                 // x46654
    new Chord("G#m",      "hard",   new[] { new Point(0,4), new Point(1,6), new Point(2,6), new Point(3,4), new Point(4,4), new Point(5,4) }), // 466444
    new Chord("Bb",       "hard",   new[] { new Point(1,1), new Point(2,3), new Point(3,3), new Point(4,3), new Point(5,1) }),                 // x13331
    new Chord("Eb",       "hard",   new[] { new Point(1,6), new Point(2,8), new Point(3,8), new Point(4,8), new Point(5,6) }),                 // x68886 (A-shape barre)
    new Chord("Ab",       "hard",   new[] { new Point(0,4), new Point(1,6), new Point(2,6), new Point(3,5), new Point(4,4), new Point(5,4) }), // 466544
    new Chord("Fm",       "hard",   new[] { new Point(0,1), new Point(1,3), new Point(2,3), new Point(3,1), new Point(4,1), new Point(5,1) }), // 133111
    new Chord("Cm",       "hard",   new[] { new Point(1,3), new Point(2,5), new Point(3,5), new Point(4,4), new Point(5,3) }),                 // x35543
    new Chord("B",        "hard",   new[] { new Point(1,2), new Point(2,4), new Point(3,4), new Point(4,4), new Point(5,2) }),                 // x24442
};

app.MapGet("/getChords", (string? difficulty, int? count) =>
{
    if (difficulty == "all") return Results.Ok(chords);
    List<Chord> chordList = new List<Chord>();

    if (difficulty == "easy")
    {
        for (int i = 0; i < chords.Count; i++)
        {
            if (chords[i].Difficulty == "easy")
            {
                chordList.Add(chords[i]);
            }
        }
    }

    else if (difficulty == "medium")
    {
        for (int i = 0; i < chords.Count; i++)
        {
            if (chords[i].Difficulty == "medium")
            {
                chordList.Add(chords[i]);
            }
        }
    }
    else if (difficulty == "hard")
    {
        for (int i = 0; i < chords.Count; i++)
        {
            if (chords[i].Difficulty == "hard")
            {
                chordList.Add(chords[i]);
            }
        }
    }

    if (count != null)
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        while (chordList.Count > count)
        {
            int indexToRemove = rnd.Next(chordList.Count);
            chordList.RemoveAt(indexToRemove);
        }
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