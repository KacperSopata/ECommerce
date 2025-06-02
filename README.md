## 🚀 CI/CD - Automatyczne wdrażanie

Projekt wykorzystuje GitHub Actions do automatycznego budowania i wdrażania aplikacji Web API po każdym pushu na gałąź `master`.

### Etapy workflow:
- ✅ Pobranie kodu z repozytorium
- ✅ Budowanie obrazu Docker przy użyciu Docker Buildx
- ✅ Wysyłka obrazu do Docker Hub
- ✅ Automatyczne wdrożenie na [Render](https://render.com)

Plik workflow znajduje się w `.github/workflows/deploy.yml`.

Wdrożona aplikacja dostępna jest pod adresem:  
👉 [https://ecommerce-xpe5.onrender.com/api/products](https://ecommerce-xpe5.onrender.com/api/products)
