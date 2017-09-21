namespace TesteFiap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class IngredienteSplitterTest
    {
        [Fact]
        public void dado_um_texto_de_ingredientes_retornar_uma_lista_vazia()
        {
            var texto = string.Empty;

            Splitter splitter = new Splitter();
            var result = splitter.Split(texto);

            Assert.IsType<List<string>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void dado_um_texto_com_dois_ingredientes_seperados_por_virgula_retornar_uma_lista_com_os_ingredientes()
        {
            var texto = "farinha,açúcar";

            Splitter splitter = new Splitter();
            var result = splitter.Split(texto);

            Assert.Equal(2, result.Count);
            Assert.Equal("farinha", result[0]);
            Assert.Equal("açúcar", result[1]);
        }

        [Fact]
        public void dado_um_separador_e_um_texto_com_tres_ingredientes_retornar_uma_lista_com_os_ingredientes()
        {
            const int totalEsperadoDeItensDeIngredientes = 3;
            var separador = ",";
            var texto = "farinha,açúcar,manteiga";

            Splitter splitter = new Splitter(separador);
            var result = splitter.Split(texto);

            Assert.Equal(totalEsperadoDeItensDeIngredientes, result.Count);
        }

        [Fact]
        public void dado_um_separador_service_com_ponto_e_virgula_e_um_texto_retornar_uma_lista_com_os_ingredientes()
        {
            const int totalEsperadoDeItensDeIngredientes = 3;
            var mockService = new Moq.Mock<ISeparatorService>();

            mockService.Setup(a => a.GetSeparator()).Returns(';');

            var texto = "arroz;feijao;batata";
            var splitter = new Splitter(mockService.Object);
            var result = splitter.Split(texto);

            Assert.Equal(totalEsperadoDeItensDeIngredientes, result.Count);

            mockService.Verify(a => a.GetSeparator(), Moq.Times.AtLeastOnce);
        }
    }

    public interface ISeparatorService
    {
        char GetSeparator();
    }

    internal class Splitter
    {
        private string separador;
        private ISeparatorService @object;

        public Splitter()
        {
        }

        public Splitter(string separador)
        {
            this.separador = separador;
        }

        public Splitter(ISeparatorService @object)
        {
            this.@object = @object;
        }

        internal List<string> Split(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return new List<string>();

            return texto.Split(@object?.GetSeparator() ?? ',').ToList();
        }
    }
}
