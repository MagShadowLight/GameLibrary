CREATE OR ALTER PROCEDURE dbo.SelectGamesByGenre
    @Genre nvarchar(max)
AS
BEGIN
    SELECT u.GameId, u.Title, u.Developer, u.Publisher, u.ReleaseDate, u.Genre, u.Prices
    FROM dbo.Games u
    WHERE u.Genre = @Genre
    ORDER BY u.GameId
END