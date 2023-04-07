DECLARE @Flowers TABLE(Name nvarchar(20))
INSERT INTO @Flowers
VALUES ('Rose'),
('Tulip'),
('Daisy'),
('Forget-me-not'),
('Lilac'),
('Narcissus'),
('Camomile'),
('Lily of the valley')

select
    f1.Name as Name1,
    f2.Name as Name2,
    f3.Name as Name3,
    f4.Name as Name4,
    f5.Name as Name5
from @Flowers as f1
    cross join @Flowers as f2
    cross join @Flowers as f3
    cross join @Flowers as f4
    cross join @Flowers as f5
where
        f1.Name < f2.Name AND
        f2.Name < f3.Name AND
        f3.Name < f4.Name AND
        f4.Name < f5.Name
order by Name1, Name2, Name3, Name4, Name5;