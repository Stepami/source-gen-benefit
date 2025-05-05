# Как поднять это добро

1. Поставить .NET 9 SDK
2. Поставить Postgres. Ожидается дефолтный конфиг `Server=localhost;Port=5432;User Id=postgres;Password=postgres;`
3. Запустить два раза проект миграций:
```
SourceGenBenefit.Migrator sg_benefit_db1
SourceGenBenefit.Migrator sg_benefit_db2
```

Можно тыкаться в бенчи, а можно просто запускать апишки
