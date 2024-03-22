using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogDb.API.Migrations
{
    /// <inheritdoc />
    public partial class InsertDataProductsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Almoço
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Feijoada Completa', 'Prato tradicional brasileiro com feijão preto, carne de porco, linguiça, arroz, couve e laranja', 25.00, 'feijoada_completa.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Picadinho de Carne', 'Carne bovina cortada em cubos e cozida com legumes, servida com arroz e farofa', 22.50, 'picadinho_carne.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Salada Caesar com Frango Grelhado', 'Salada Caesar clássica com alface, croutons, queijo parmesão, molho Caesar e peito de frango grelhado', 18.00, 'salada_caesar_frango.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Macarrão à Bolonhesa', 'Macarrão penne servido com molho de carne moída e queijo parmesão ralado', 20.00, 'macarrao_bolonhesa.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Risoto de Frutos do Mar', 'Risoto cremoso com frutos do mar, como camarões, lulas e vieiras', 28.00, 'risoto_frutos_mar.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Lombo de Porco Assado', 'Lombo de porco assado lentamente no forno com ervas e temperos', 24.00, 'lombo_porco_assado.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Nhoque ao Molho Pesto', 'Nhoque de batata servido com molho pesto de manjericão fresco e queijo parmesão', 21.50, 'nhoque_molho_pesto.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Frango à Parmegiana', 'Peito de frango empanado e frito, coberto com molho de tomate e queijo derretido, servido com espaguete', 23.50, 'frango_parmegiana.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Peixe Grelhado com Legumes', 'Filé de peixe grelhado servido com legumes grelhados e arroz branco', 22.00, 'peixe_grelhado_legumes.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Lasanha de Frango e Espinafre', 'Lasanha com camadas de massa, frango desfiado, molho branco, espinafre e queijo', 20.00, 'lasanha_frango_espinafre.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Almoço'))");

            // Bebidas Alcoólicas
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cerveja Artesanal IPA', 'Cerveja artesanal estilo IPA 500 ml', 10.00, 'cerveja.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Vinho Tinto Seco', 'Vinho tinto seco de uvas selecionadas 750 ml', 25.00, 'vinho_tinto.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Whisky Single Malt', 'Whisky Single Malt envelhecido 12 anos 750 ml', 50.00, 'whisky.jpg', 10, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Gin Tônica', 'Drink clássico com gin, água tônica e limão', 15.00, 'gin_tonica.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Martini Seco', 'Cocktail clássico com gin e vermute seco', 12.00, 'martini_seco.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Caipirinha', 'Drink brasileiro com cachaça, limão e açúcar', 8.50, 'caipirinha.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Mojito', 'Cocktail cubano com rum, hortelã, limão, açúcar e água com gás', 10.00, 'mojito.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tequila Sunrise', 'Drink mexicano com tequila, suco de laranja e grenadine', 11.50, 'tequila_sunrise.jpg', 16, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Negroni', 'Cocktail italiano com gin, vermute tinto e Campari', 13.00, 'negroni.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Dry Martini', 'Cocktail clássico com gin e vermute seco, servido com azeitona', 14.00, 'dry_martini.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Acoólicas'))");

            // Bebidas Frias
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Refrigerante de Cola', 'Refrigerante de Cola 350 ml', 5.00, 'refrigerante_cola.jpg', 50, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Suco de Laranja', 'Suco natural de laranja 300 ml', 6.00, 'suco_laranja.jpg', 40, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Água Mineral', 'Água sem gás 500 ml', 2.50, 'agua.jpg', 100, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá Gelado de Pêssego', 'Chá gelado de pêssego com limão', 4.50, 'cha_pessego.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Café Gelado', 'Café gelado com leite e calda de caramelo', 7.00, 'cafe_gelado.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Smoothie de Frutas Vermelhas', 'Smoothie refrescante de frutas vermelhas', 8.50, 'smoothie_frutas_vermelhas.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Iced Tea', 'Chá gelado com limão e hortelã', 5.50, 'iced_tea.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Frappuccino de Chocolate', 'Bebida gelada com café, leite, e calda de chocolate', 6.75, 'frappuccino_chocolate.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Milkshake de Morango', 'Milkshake cremoso de morango com chantilly', 8.00, 'milkshake_morango.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Limonada', 'Limão espremido com água e açúcar', 4.00, 'limonada.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Frias'))");

            // Bebidas Quentes
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Café Expresso', 'Café expresso de alta qualidade', 3.50, 'cafe_expresso.jpg', 50, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá de Camomila', 'Chá de camomila calmante e relaxante', 2.50, 'cha_camomila.jpg', 40, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chocolate Quente', 'Chocolate quente cremoso com marshmallow', 4.50, 'chocolate_quente.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá de Hortelã', 'Chá de hortelã refrescante', 3.00, 'cha_hortela.jpg', 45, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cappuccino', 'Cappuccino cremoso com canela', 4.75, 'cappuccino.jpg', 35, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá de Frutas Vermelhas', 'Chá de frutas vermelhas aromático', 3.25, 'cha_frutas_vermelhas.jpg', 38, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Café com Leite', 'Café com leite cremoso e espumante', 4.00, 'cafe_com_leite.jpg', 42, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá Verde', 'Chá verde tradicional', 3.00, 'cha_verde.jpg', 40, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Café Americano', 'Café americano suave e encorpado', 3.25, 'cafe_americano.jpg', 48, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chá Preto', 'Chá preto forte e encorpado', 2.75, 'cha_preto.jpg', 36, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Bebidas Quentes'))");

            // Café da Manhã
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Café Americano', 'Café preto tradicional', 3.50, 'cafe_americano.jpg', 50, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pão Francês', 'Pão francês fresco', 2.00, 'pao_frances.jpg', 100, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Croissant de Chocolate', 'Croissant recheado com chocolate', 3.50, 'croissant_chocolate.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torrada de Abacate', 'Torrada de pão integral com abacate amassado', 5.00, 'torrada_abacate.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Omelete de Queijo', 'Omelete de queijo com tomate e cebola', 8.50, 'omelete_queijo.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Açaí na Tigela', 'Açaí batido com banana e granola', 10.00, 'acai_tigela.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Iogurte com Granola', 'Iogurte natural com granola e frutas', 6.00, 'iogurte_granola.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Panqueca de Banana', 'Panqueca de banana com mel e canela', 7.50, 'panqueca_banana.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tapioca de Queijo', 'Tapioca recheada com queijo coalho', 6.00, 'tapioca_queijo.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Smoothie de Frutas Vermelhas', 'Smoothie refrescante feito com frutas vermelhas congeladas e leite de amêndoas', 9.50, 'smoothie_frutas_vermelhas.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Café da Manhã'))");

            // Janta
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Strogonoff de Frango', 'Strogonoff de frango com arroz branco', 15.00, 'strogonoff_frango.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Spaghetti à Carbonara', 'Spaghetti com molho à Carbonara', 14.00, 'spaghetti_carbonara.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Salmão Grelhado', 'Salmão grelhado com legumes ao vapor', 18.00, 'salmao_grelhado.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tacos de Carne', 'Tacos mexicanos com carne moída e guacamole', 12.50, 'tacos_carne.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Peixe Assado', 'Peixe branco assado com batatas e ervas', 16.00, 'peixe_assado.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Bife à Milanesa', 'Bife empanado e frito, acompanhado de arroz e feijão', 17.50, 'bife_milanesa.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Frango à Parmegiana', 'Peito de frango empanado, coberto com molho de tomate e queijo derretido', 19.00, 'frango_parmegiana.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Costela Bovina Assada', 'Costela bovina assada lentamente, servida com molho barbecue', 22.00, 'costela_assada.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Risoto de Camarão', 'Risoto cremoso com camarões frescos', 20.50, 'risoto_camarao.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Feijoada Completa', 'Feijoada tradicional com linguiça, carne seca, costelinha de porco e acompanhamentos', 25.00, 'feijoada_completa.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Janta'))");

            // Lanches
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hambúrguer Clássico', 'Hambúrguer de carne bovina grelhado com queijo, alface, tomate e molho especial', 12.00, 'hamburguer_classico.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sanduíche de Frango', 'Sanduíche de frango grelhado com alface, tomate, queijo e maionese', 10.50, 'sanduiche_frango.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cachorro-Quente', 'Pão com salsicha, molho de tomate, mostarda, ketchup, milho, ervilha e batata palha', 8.00, 'cachorro_quente.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Wrap de Frango', 'Wrap recheado com frango grelhado, alface, tomate e molho', 9.00, 'wrap_frango.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hot Dog', 'Pão com salsicha, molho de tomate, mostarda e ketchup', 7.50, 'hotdog.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cheeseburger Bacon', 'Hambúrguer de carne bovina grelhado com queijo, bacon, alface, tomate e maionese', 13.50, 'cheeseburger_bacon.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sanduíche de Presunto e Queijo', 'Sanduíche com presunto, queijo, alface, tomate e maionese', 9.00, 'sanduiche_presunto_queijo.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Panini de Salame e Queijo', 'Panini recheado com salame, queijo e molho especial', 10.00, 'panini_salame_queijo.jpg', 26, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torrada Mista', 'Torrada com presunto, queijo e molho especial', 6.50, 'torrada_mista.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sanduíche de Carne Assada', 'Sanduíche com carne assada, queijo, alface, tomate e molho especial', 11.00, 'sanduiche_carne_assada.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Lanches'))");

            // Petiscos
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Aperitivo de Azeitona com Salame e Queijo', 'Azeitonas, salame e queijo em um aperitivo delicioso', 11.00, 'aperitivo_azeitona_salame_queijo.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Bolinho de Arroz', 'Bolinhos de arroz fritos e crocantes', 8.50, 'bolinho_arroz.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Chips de Provolone', 'Fatias de provolone crocante e salgado', 9.00, 'chips_provolone.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cestinha de Frios', 'Cesta de pães recheada com uma variedade de frios e queijos', 12.50, 'cestinha_frios.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Petisco de Salame com Ovo de Codorna', 'Fatias de salame com ovos de codorna', 10.00, 'petisco_salame_ovo_codorna.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Batata Rústica Assada', 'Batatas rústicas assadas com temperos', 7.50, 'batata_rustica_assada.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Batata Brava', 'Batatas fritas com molho picante', 8.00, 'batata_brava.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Batatinha com Bacon e Cheddar', 'Batatas fritas com bacon e queijo cheddar derretido', 9.50, 'batatinha_bacon_cheddar.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pãozinho de Cebola', 'Pãozinho assado com recheio de cebola caramelizada', 6.00, 'paozinho_cebola.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torrada com Azeite e Alho', 'Fatias de pão torrado com azeite e alho', 5.50, 'torrada_azeite_alho.jpg', 35, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Petiscos'))");

            // Sobremesas
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cheesecake de Morango', 'Cheesecake cremoso com cobertura de morangos frescos', 12.00, 'cheesecake_morango.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Mousse de Chocolate', 'Mousse de chocolate cremoso e aerado', 8.50, 'mousse_chocolate.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torta de Limão', 'Torta de limão com massa crocante e recheio de limão cremoso', 10.00, 'torta_limao.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pudim de Leite Condensado', 'Pudim de leite condensado cremoso e caramelizado', 9.50, 'pudim_leite_condensado.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tiramisu', 'Sobremesa italiana com camadas de biscoitos, café, mascarpone e cacau', 13.00, 'tiramisu.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pavê de Chocolate', 'Pavê de chocolate com camadas de biscoito, creme e calda de chocolate', 11.50, 'pave_chocolate.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sorvete de Baunilha', 'Sorvete cremoso de baunilha', 6.00, 'sorvete_baunilha.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Brownie com Sorvete', 'Brownie quente e macio servido com uma bola de sorvete de baunilha', 10.50, 'brownie_sorvete.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Milkshake de Morango', 'Milkshake cremoso de morango com chantilly', 7.50, 'milkshake_morango.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Frutas da Estação', 'Seleção de frutas frescas da estação', 9.00, 'frutas_estacao.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sobremesas'))");

            // Sopas e Caldos
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sopa de Legumes', 'Sopa cremosa de legumes frescos', 8.00, 'sopa_legumes.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Caldo Verde', 'Caldo tradicional português com batatas, couve e chouriço', 9.50, 'caldo_verde.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Canja de Galinha', 'Sopa de arroz com pedaços de frango, cenoura e ervas', 10.00, 'canja_galinha.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sopa de Cebola', 'Sopa de cebola gratinada com queijo', 9.00, 'sopa_cebola.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Creme de Aspargos', 'Creme de aspargos frescos com um toque de creme de leite', 11.50, 'creme_aspargos.jpg', 18, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sopa de Tomate', 'Sopa de tomate cremosa com manjericão fresco', 10.50, 'sopa_tomate.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Creme de Milho', 'Creme de milho com pedaços de milho fresco', 9.00, 'creme_milho.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sopa de Feijão', 'Sopa de feijão com bacon e linguiça defumada', 10.00, 'sopa_feijao.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Caldo de Mandioca com Carne', 'Caldo quente de mandioca com pedaços de carne', 11.50, 'caldo_mandioca_carne.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Sopa de Lentilha', 'Sopa reconfortante de lentilha com temperos tradicionais', 10.00, 'sopa_lentilha.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Sopas e Caldos'))");

            // Vegetarianos e Veganos
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hambúrguer de Grão-de-Bico', 'Hambúrguer vegetariano feito com grão-de-bico e temperos naturais', 11.00, 'hamburguer_grao_bico.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Lasanha de Berinjela', 'Lasanha vegetariana feita com fatias de berinjela, molho de tomate e queijo vegano', 13.50, 'lasanha_berinjela.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Risoto de Cogumelos', 'Risoto cremoso de cogumelos frescos e arroz arbóreo', 12.00, 'risoto_cogumelos.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hambúrguer de Lentilha', 'Hambúrguer vegetariano feito com lentilhas, cenoura e temperos', 10.50, 'hamburguer_lentilha.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torta de Legumes', 'Torta salgada recheada com legumes frescos e queijo vegano', 11.00, 'torta_legumes.jpg', 26, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hambúrguer de Quinoa', 'Hambúrguer vegetariano feito com quinoa, feijão preto e vegetais', 12.50, 'hamburguer_quinoa.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pad Thai Vegetariano', 'Prato tailandês de macarrão de arroz, legumes, tofu e molho agridoce', 14.00, 'pad_thai_vegetariano.jpg', 22, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Pizza Vegetariana', 'Pizza com molho de tomate, queijo vegano, pimentão, cebola e cogumelos', 15.00, 'pizza_vegetariana.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Coxinha de Jaca', 'Coxinha vegana feita com jaca verde desfiada e temperada', 8.50, 'coxinha_jaca.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torta de Espinafre', 'Torta salgada recheada com espinafre, queijo vegano e tomate', 10.50, 'torta_espinafre.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Hambúrguer de Feijão Preto', 'Hambúrguer vegetariano feito com feijão preto, cenoura e temperos', 10.00, 'hamburguer_feijao_preto.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Wrap de Falafel', 'Wrap recheado com falafel, vegetais frescos e molho de tahine', 9.50, 'wrap_falafel.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Quibe Vegetariano', 'Quibe assado recheado com proteína de soja e temperos árabes', 11.00, 'quibe_vegetariano.jpg', 28, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Cuscuz de Legumes', 'Cuscuz marroquino com legumes grelhados e especiarias', 12.50, 'cuscuz_legumes.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Espaguete de Abobrinha', 'Espaguete de abobrinha servido com molho de tomate e manjericão', 9.00, 'espaguete_abobrinha.jpg', 30, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Burrito Vegetariano', 'Burrito recheado com feijão, arroz, legumes e guacamole', 12.00, 'burrito_vegetariano.jpg', 26, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Tofu Grelhado', 'Tofu marinado grelhado com legumes ao vapor', 13.00, 'tofu_grelhado.jpg', 24, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Salada de Quinoa', 'Salada de quinoa com vegetais frescos e molho de limão', 10.00, 'salada_quinoa.jpg', 25, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Espaguete de Abóbora com Molho Pesto', 'Espaguete de abóbora servido com molho pesto vegano', 10.50, 'espaguete_abobora_pesto.jpg', 20, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, ImageUrl, Stock, RegistrationDate, CategoryId)" +
                "VALUES('Torta de Legumes sem Glúten', 'Torta salgada sem glúten recheada com legumes frescos e queijo vegano', 13.00, 'torta_legumes_sem_gluten.jpg', 15, GETDATE(), (SELECT Id FROM Categories WHERE Name = 'Vegetarianos e Veganos'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}
