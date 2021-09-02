CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(  
    id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title varchar(255) NOT NULL comment 'recipe title',
    description varchar(255) comment 'description',
    cookTime int   comment 'cook in Minutes',
    prepTime int   comment 'prep in Minutes',
    creatorId VARCHAR(255) NOT NULL comment 'creator info',
    

FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';


CREATE TABLE IF NOT EXISTS steps(  
    id int NOT NULL primary key AUTO_INCREMENT comment 'primary key',
    body varchar(255) NOT NULL comment 'recipe title',    
    creatorId VARCHAR(255) NOT NULL comment 'creator info',
    recipeId int NOT NULL comment 'recipe info',
    

FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';