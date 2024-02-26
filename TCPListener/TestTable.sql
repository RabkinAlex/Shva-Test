USE [Test]
GO

/****** Object:  Table [dbo].[TestTable]    Script Date: 2/26/2024 1:08:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TestTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EncData] [nvarchar](max) NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_TestTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[TestTable] ADD  CONSTRAINT [DF_TestTable_Created]  DEFAULT (getdate()) FOR [Created]
GO

