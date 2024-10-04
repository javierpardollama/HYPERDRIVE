import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { BatchSpanProcessor, WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { getWebAutoInstrumentations } from '@opentelemetry/auto-instrumentations-web';
import { OTLPTraceExporter } from '@opentelemetry/exporter-trace-otlp-http';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { environment } from './environments/environment';

const provider = new WebTracerProvider();

// Batch traces before sending them to Aspire
provider.addSpanProcessor(
    new BatchSpanProcessor(
        new OTLPTraceExporter({
            url: `${environment.Otel.Exporter}`,
            headers: {
                'x-otlp-api-key': environment.Otel.Key              
            },
        }),
    ),
);

provider.register({
    contextManager: new ZoneContextManager(),
});

registerInstrumentations({
    instrumentations: [
        getWebAutoInstrumentations({
            '@opentelemetry/instrumentation-document-load': {},
            '@opentelemetry/instrumentation-user-interaction': {},
            '@opentelemetry/instrumentation-fetch': {},
            '@opentelemetry/instrumentation-xml-http-request': {},
        }),
    ],
});