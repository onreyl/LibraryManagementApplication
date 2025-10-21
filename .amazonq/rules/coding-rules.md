# Proje Kodlama Kuralları

## Temel Kurallar
- Kullanıcıdan onay almadan kod ekleme veya değiştirme
- Sadece sorulan soruyu yanıtla, ekstra özellik ekleme
- Minimal kod yaz, gereksiz implementasyon yapma
- Mevcut kodu değiştirmeden önce kullanıcıya sor

## Clean Architecture Kuralları
- Domain katmanı diğer katmanlara bağımlı olmamalı
- Infrastructure katmanı sadece Domain'e bağımlı olmalı
- Application katmanı sadece Domain'e bağımlı olmalı
- API katmanı tüm katmanlara referans verebilir

## Entity Framework Kuralları
- Repository pattern kullan
- DbContext'i Infrastructure katmanında tut
- Migration'ları Infrastructure katmanında yönet

## Naming Conventions
- PascalCase: Class, Method, Property isimleri
- camelCase: Field ve parameter isimleri
- Interface isimleri 'I' ile başlamalı
- Async method isimleri 'Async' ile bitmeli