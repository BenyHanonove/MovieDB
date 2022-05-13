CREATE TABLE [Movies_2022] (
		[id] tinyint NOT NULL UNIQUE ,
        [title] nvarchar (255) NOT NULL ,
        [description] nvarchar (500) NOT NULL,
		[genre] nvarchar (100) NOT NULL,
		[releaseDate] DATE not NULL,
		[img] nvarchar (255) NOT NULL,
		[score] float NOT NULL,
	Primary key (id)
)

INSERT INTO [Movies_2022]
           (
		   [id],
            [title],
			[description],
			[genre],
			[releaseDate],
			[img],
			[score]
           )
     VALUES
           (318455250,
		   'Title',
           	'Description',
			'Genre',
			'5/1/2019',
			'Img',
			7.8
	   )

	   select *
	   from Movies_2022


