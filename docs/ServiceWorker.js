const cacheName = "m3huul-SpaceShooter-1.1";
const contentToCache = [
    "Build/docs.loader.js",
    "Build/docs.framework.js",
    "Build/docs.data",
    "Build/docs.wasm",
];

self.addEventListener('install', function (e) {
    console.log('[Service Worker] Install');
    
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
});

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
