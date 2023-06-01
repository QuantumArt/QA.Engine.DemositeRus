export const environment = {
  production: true,
  WIDGET_PLATFORM_API_URL: process.env['WIDGET_PLATFORM_API_URL'] || 'http://localhost',
  GRAPHQL_DATA_API_URL: process.env['GRAPHQL_DATA_API_URL'] || 'http://localhost/graphql',
  FEEDBACK_API_URL: process.env['FEEDBACK_API_URL'] || 'feedback/sendfeedback',
  SUBSCRIBE_API_URL: process.env['SUBSCRIBE_API_URL'] || 'subscribe/add',
};
