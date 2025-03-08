#if USE_DATA_CACHING
const cacheName = {{{JSON.stringify(COMPANY_NAME + "-" + PRODUCT_NAME + "-" + PRODUCT_VERSION )}}};
const contentToCache = [
    "Build/{{{ LOADER_FILENAME }}}",
    "Build/{{{ FRAMEWORK_FILENAME }}}",
#if USE_THREADS
    "Build/{{{ WORKER_FILENAME }}}",
#endif
    "Build/{{{ DATA_FILENAME }}}",
    "Build/{{{ CODE_FILENAME }}}",
    "TemplateData/style.css"

];
#endif

self.addEventListener('install', function (e) {
    console.log('[Service Worker] Install');
    
#if USE_DATA_CACHING
    e.waitUntil((async function () {
      try {
          const cache = await caches.open(cacheName);
          console.log('[Service Worker] Caching all: app shell and content');
          for (const resource of contentToCache) {
              try {
                  await cache.add(resource);
              } catch (error) {
                  console.error(`[Service Worker] Failed to cache: ${resource}`, error);
              }
          }
      } catch (error) {
          console.error('[Service Worker] Failed to open cache', error);
      }
    })());
#endif
});

#if USE_DATA_CACHING
self.addEventListener('fetch', function (e) {
  console.log("Called")
  e.respondWith((async function () {
     try {
         const response = await caches.match(e.request);
         console.log(`[Service Worker] Fetching resource: ${e.request.url}`);
         if (response) return response;

         const fetchResponse = await fetch(e.request);
         const cache = await caches.open(cacheName);
         console.log(`[Service Worker] Caching new resource: ${e.request.url}`);
         await cache.put(e.request, fetchResponse.clone());
         return fetchResponse;
     } catch (error) {
         console.error(`[Service Worker] Error fetching resource: ${e.request.url}`, error);
         throw error;
     }
 })());
});
#endif
