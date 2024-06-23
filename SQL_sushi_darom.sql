-- Создание таблицы Logpass
CREATE TABLE Logpass (
    id INT IDENTITY(1,1) PRIMARY KEY,
    phone_number VARCHAR(15) NOT NULL,
    password_ VARCHAR(255) NOT NULL,
    CONSTRAINT UQ_PhoneNumber UNIQUE (phone_number)
);

-- Создание таблицы Customers
CREATE TABLE Customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    name_cm VARCHAR(100) NOT NULL,
    phone_number VARCHAR(15),
    address_ VARCHAR(255),
    FOREIGN KEY (phone_number) REFERENCES Logpass(phone_number)
);

-- Создание таблицы "Category" (категории товаров)
CREATE TABLE Category 
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name_category VARCHAR(100) NOT NULL
);

-- Создание таблицы "Products" (позиции товаров)
CREATE TABLE Products 
(
    id_products INT PRIMARY KEY IDENTITY(1,1),
    name_pr VARCHAR(100) NOT NULL,
    price_pr DECIMAL(10, 2) NOT NULL,
    description_pr VARCHAR(500),
    picture VARCHAR(300),
    id_category INT,
    FOREIGN KEY (id_category) REFERENCES Category(id)
);

CREATE TABLE Orders 
(
    order_id INT PRIMARY KEY IDENTITY(1,1),
    customer_name VARCHAR(100) NOT NULL,
    order_date DATE,
    order_status VARCHAR(50),
	comment_ord VARCHAR(256),
    total_sum DECIMAL(10, 2) -- новая колонка для общей суммы
);

-- Создание таблицы OrdersDetails
CREATE TABLE OrdersDetails 
(
    order_id INT,
    product_name VARCHAR(100) NOT NULL,
    product_price DECIMAL(10, 2) NOT NULL,
    order_date DATETIME,
	address_ VARCHAR(255),
    FOREIGN KEY (order_id) REFERENCES Orders(order_id)
);

--------------------------------------------

-- Пример вставки данных в таблицу Logpass
INSERT INTO Logpass (phone_number, password_)
VALUES 
('admin', '1');

-- Добавление данных в таблицу Customers
INSERT INTO Customers (name_cm, phone_number, address_)
VALUES 
('Администратор', 'admin', 'г. Москва, ул. Пушкина, д.1');

-- Добавление данных в таблицу Category
INSERT INTO Category (name_category)
VALUES
    ('Сеты'),
    ('Роллы'),
    ('Напитки'),
	('Суши');

-- Добавление данных в таблицу Products
INSERT INTO Products (name_pr, price_pr, description_pr, picture, id_category)
VALUES
    ('Сет Большой', 1500, '7 ПОРЦИЙ: БЭБИ МАКИ, УНАГИ ФУРАЙ, СУХАРЬ МАКИ,ТАЙ МАКИ,
ФИЛАДЕЛЬФИЯ С ОГУРЦОМ,ТОКИО, БИГ КАППА С СЫРОМ', 'C:/sushi_darom/img/setBig.png', 1),
    ('Ролл Калифорния', 250, 'РОЛЛ С ТИГРОВОЙ КРЕВЕТКОЙ, ОГУРЦОМ, СЛИВОЧНЫМ СЫРОМ, АВОКАДО. УКРАШЕН ИКРОЙ "МАСАГО"', 'C:/sushi_darom/img/rollCalifornia.png', 2),
	('Кока-кола', 75, 'В АССОРТИМЕНТЕ', 'C:/sushi_darom/img/CocaCola.png', 3),
	('Фанта', 95, 'В АССОРТИМЕНТЕ', 'C:/sushi_darom/img/Fanta.png', 3);

---- Вставка данных в таблицу Orders
--INSERT INTO Orders (customer_name, order_date, order_status, total_sum)
--VALUES 
--('Антон', GETDATE(), 'В обработке', 1750); -- общая сумма заказа

---- Вставка данных в таблицу OrdersDetails
--INSERT INTO OrdersDetails (order_id, product_name, product_price, order_date, address_)
--VALUES 
--(1, 'Ролл Калифорния', 250, GETDATE(), 'В обработке', 'Серпухов'),
--(1, 'Сет Большой', 1500, GETDATE(), 'В обработке', 'Серпухов');

---- КОМАНДЫ К БАЗЕ ДАННЫХ --

---- Выгрузка всех колонок по таблице --
--SELECT * FROM Logpass
---- Выгрузка данных позиций по Сетам --
--SELECT id_products, name_, price, description_, picture FROM Products JOIN Category ON Products.id_category = Category.id WHERE Category.name_category = 'Сеты'
---- Удаление таблицы --
--drop table Products

--SELECT * FROM OrdersDetails WHERE order_id = 1
--SELECT * FROM Orders
--SELECT * FROM OrdersDetails