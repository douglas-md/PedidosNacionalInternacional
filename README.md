
## Decisões de Design

**Herança por especialização:** A classe base `Pedido` define o ritual fixo `Processar()` (Validar → Calcular → Emitir) e expõe ganchos `protected virtual` para que `PedidoNacional` e `PedidoInternacional` especializem apenas o cálculo de impostos/taxas e o formato do recibo, sem quebrar o contrato (LSP).

**Composição via delegates:** Políticas de Frete e Promoção são eixos independentes injetados como `Func<decimal, decimal>` no construtor. Isso permite trocar estratégias (frete fixo/percentual, cupons variados) sem criar subclasses combinatórias como `PedidoNacionalComFreteFixoComCupom10`.

**Testes:** Validam que o cliente pode usar `Pedido` genérico e funciona com Nacional/Internacional sem `is`/downcast (LSP), e que a troca de delegates altera comportamento sem novas classes (composição).


Douglas - POO - CC - UTFPR 
