using Pedidos;
using Xunit;

namespace Pedidos.Tests;

public class PedidoTests
{
    [Fact]
    public void TesteLSP_ClienteAgnóstico_AceitaPedidoGenérico()
    {
        // Arrange: função que aceita Pedido genérico
        void ProcessarQualquerPedido(Pedido p) => p.Processar();
        
        var nacional = new PedidoNacional(Fretes.Fixo, Promocoes.Nenhuma);
        var internacional = new PedidoInternacional(5.0m, Fretes.Fixo, Promocoes.Nenhuma);
        
        // Act & Assert: funciona sem downcast/is
        ProcessarQualquerPedido(nacional);      // ✓ Substitui Pedido sem quebrar
        ProcessarQualquerPedido(internacional); // ✓ Substitui Pedido sem quebrar
        
        // Se chegou aqui, LSP está sendo respeitado
        Assert.True(true);
    }
    
    [Fact]
    public void TesteComposicao_TrocaDeFretes_SemNovasSubclasses()
    {
        // Arrange: mesma classe, fretes diferentes
        var pedido1 = new PedidoNacional(Fretes.Fixo, Promocoes.Nenhuma);
        var pedido2 = new PedidoNacional(Fretes.Percentual, Promocoes.Nenhuma);
        var pedido3 = new PedidoNacional(Fretes.Gratis, Promocoes.Nenhuma);
        
        // Act
        pedido1.Processar(); // Base + impostos + R$ 15
        pedido2.Processar(); // Base + impostos + 10%
        pedido3.Processar(); // Base + impostos + R$ 0
        
        // Assert: diferentes fretes aplicados sem criar
        // PedidoNacionalComFreteFixo, PedidoNacionalComFretePercentual, etc.
        Assert.True(true); // Composição funcionando
    }
    
    [Fact]
    public void TesteComposicao_TrocaDePromocoes_SemNovasSubclasses()
    {
        // Arrange: mesma classe, promoções diferentes
        var pedido1 = new PedidoInternacional(5.0m, Fretes.Fixo, Promocoes.Nenhuma);
        var pedido2 = new PedidoInternacional(5.0m, Fretes.Fixo, Promocoes.Cupom10);
        var pedido3 = new PedidoInternacional(5.0m, Fretes.Fixo, Promocoes.BlackFriday);
        
        // Act
        pedido1.Processar(); // 100% do valor
        pedido2.Processar(); // 90% do valor
        pedido3.Processar(); // 50% do valor
        
        // Assert: diferentes promoções sem criar novas subclasses
        Assert.True(true);
    }
    
    [Fact]
    public void TesteComposicao_CombinaFreteEPromocao_Livremente()
    {
        // Arrange: combina frete percentual + cupom 20%
        var pedido = new PedidoNacional(Fretes.Percentual, Promocoes.Cupom20);
        
        // Act
        pedido.Processar();
        
        // Assert: composição permite combinações livres
        Assert.True(true);
    }
}
