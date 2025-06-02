## 🚀 CI/CD - Automatyczne wdrażanie

Projekt wykorzystuje GitHub Actions do automatycznego budowania i wdrażania aplikacji Web API po każdym pushu na gałąź `master`.

### Etapy workflow:
- ✅ Pobranie kodu z repozytorium
- ✅ Budowanie aplikacji
- ✅ Budowanie obrazu Docker przy użyciu Docker Buildx
- ✅ Wysyłka obrazu do Docker Hub
- ✅ Automatyczne wdrożenie na [Render](https://render.com)
- ✅ Publikacja bezpośrednio na Azure App Service (Web Deploy)

Plik workflow znajduje się w `.github/workflows/deploy.yml`.

---

## ☁️ Wdrożenie API w chmurze Azure

Aplikacja została również wdrożona w **Azure App Service** – publicznie dostępna i gotowa do testów.

### 🔧 Wykorzystane usługi Azure:
- **Azure App Service** – hosting aplikacji Web API (.NET 8)
- **Azure Resource Group** – zarządzanie zasobami
- **Azure Portal** – konfiguracja i monitorowanie wdrożenia

### 🔗 Dostęp do aplikacji:

#### Swagger UI:
👉 [https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/swagger](https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/swagger)

#### Endpointy:
- Produkty:  
  [https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/api/products](https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/api/products)
- Zamówienia:  
  [https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/api/orders](https://ecommerce-a0atcweaf6dbedfp.westeurope-01.azurewebsites.net/api/orders)

---

## 🌐 Alternatywne środowisko: Render

Aplikacja jest również automatycznie wdrażana do [Render](https://render.com) po każdym pushu.

### Endpointy Render:
- Swagger UI:  
  👉 [https://ecommerce-xpe5.onrender.com/swagger](https://ecommerce-xpe5.onrender.com/swagger)  
- Produkty:  
  [https://ecommerce-xpe5.onrender.com/api/products](https://ecommerce-xpe5.onrender.com/api/products)
- Zamówienia:  
  [https://ecommerce-xpe5.onrender.com/api/orders](https://ecommerce-xpe5.onrender.com/api/orders)

---

## ⚙️ Informacje konfiguracyjne

- Wszystkie ustawienia połączenia z bazą danych są w pliku `appsettings.json`
- Po stronie Azure publikacja jest wykonywana z poziomu Visual Studio (Web Deploy)
- Render używa obrazu Docker oraz `deploy.yml` do automatycznego builda i wdrożenia
