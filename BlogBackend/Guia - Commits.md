
# 🧾 Guía de Commits Profesionales (Conventional Commits)

> Usa esta plantilla como referencia para escribir mensajes de commit limpios, claros y útiles en cualquier equipo de desarrollo.

---

## 🔧 Estructura básica

```bash
<tipo>([alcance opcional]): descripción breve en minúscula y modo imperativo

[Descripción extendida opcional si es necesaria]
```

---

## 🚀 `feat`: Nuevas funcionalidades

| Ejemplo | Descripción |
|--------|-------------|
| `feat(auth): agregar autenticación con JWT` | Añade login con tokens |
| `feat(user): crear endpoint de registro de usuarios` | Endpoint nuevo |
| `feat(posts): permitir creación de posts` | Nueva funcionalidad de posteo |
| `feat(profile): agregar avatar de usuario` | Nuevo campo |
| `feat(redis): integrar Redis para caching de sesiones` | Cache |
| `feat(email): enviar correo de verificación al registrar` | Notificación |
| `feat(ui): agregar tema oscuro` | Mejora de interfaz |
| `feat(admin): panel de control para moderadores` | Herramienta nueva |
| `feat(tags): soporte para etiquetas en publicaciones` | Funcionalidad SEO |
| `feat(security): permitir autenticación 2FA` | Seguridad |
| `feat(comments): añadir sección de comentarios` | Interacción |

---

## 🐛 `fix`: Corrección de errores

| Ejemplo | Descripción |
|--------|-------------|
| `fix(auth): corregir validación de token expirado` | Bug de auth |
| `fix(user): evitar crash si email es null` | Null check |
| `fix(posts): fecha incorrecta en post nuevo` | Bug de timestamp |
| `fix(cache): evitar duplicado de claves en Redis` | Key conflict |
| `fix(api): error 500 al crear recurso vacío` | Manejo de errores |
| `fix(cors): permitir solo dominios seguros` | Seguridad |
| `fix(form): validación de campos requeridos` | UX |
| `fix(upload): error al subir imágenes grandes` | Tamaño máximo |
| `fix(route): redirección incorrecta tras login` | Navegación |
| `fix(env): variable de entorno mal escrita` | Config |
| `fix(db): evitar inserción duplicada de usuarios` | DB integrity |

---

## 🧪 `test`: Pruebas

| Ejemplo | Descripción |
|--------|-------------|
| `test(auth): agregar pruebas unitarias al servicio` | Cobertura |
| `test(user): pruebas de integración con base de datos` | Test real |
| `test(api): mock de endpoints para CI` | Mocking |
| `test(jwt): verificar expiración de tokens` | Seguridad |
| `test(redis): testear caché de usuarios` | Rendimiento |
| `test(posts): cobertura de postController` | Controlador |
| `test(login): testear credenciales inválidas` | Edge case |
| `test(routes): verificar rutas públicas/privadas` | Roles |
| `test(errors): probar manejo de excepciones` | Resiliencia |
| `test(utils): testear función de hashing` | Utilidades |
| `test(file): pruebas de subida y validación de archivos` | Archivos |

---

## 🧼 `refactor`: Mejora de código sin cambiar lógica

| Ejemplo | Descripción |
|--------|-------------|
| `refactor(auth): extraer validaciones en clase separada` | Limpieza |
| `refactor(user): renombrar campos para mayor claridad` | Naming |
| `refactor(jwt): mover lógica de generación a servicio` | Modulo |
| `refactor(db): reorganizar estructura de carpetas` | Organización |
| `refactor(posts): eliminar duplicación de lógica` | DRY |
| `refactor(api): dividir controlador largo en archivos` | Legibilidad |
| `refactor(config): usar constantes en lugar de strings` | Seguridad |
| `refactor(logger): reemplazar console.log por logger` | Profesionalismo |
| `refactor(middleware): simplificar flujo de autorización` | UX dev |
| `refactor(types): usar tipos definidos en lugar de `any`` | TS |
| `refactor(repo): inyectar dependencias en lugar de instanciar` | SOLID |

---

## 💅 `style`: Formato y estilo

| Ejemplo | Descripción |
|--------|-------------|
| `style(auth): aplicar Prettier a servicios` | Formato |
| `style(posts): corregir indentación` | Limpieza |
| `style(routes): usar comillas simples` | Estilo |
| `style(api): eliminar líneas innecesarias` | Minimalismo |
| `style(env): ordenar claves alfabéticamente` | Orden |
| `style(docs): formatear Markdown` | Docs |
| `style(types): aplicar regla de linters` | TSLint/ESLint |
| `style(html): aplicar formato con beautify` | HTML |
| `style(config): estandarizar nombres de variables` | Consistencia |
| `style(logger): formatear salida de logs` | UX dev |
| `style(ci): alineación de YAML en GitHub Actions` | CI legible |

---

## 📚 `docs`: Documentación

| Ejemplo | Descripción |
|--------|-------------|
| `docs(readme): agregar instrucciones de instalación` | Setup |
| `docs(auth): documentar flujo de autenticación JWT` | Técnica |
| `docs(routes): detallar endpoints disponibles` | API |
| `docs(changelog): añadir cambios de versión` | Historial |
| `docs(env): explicar uso de variables de entorno` | DevOps |
| `docs(db): describir modelo de base de datos` | Arquitectura |
| `docs(contributing): guía para nuevos contribuidores` | Comunidad |
| `docs(security): políticas básicas de seguridad` | Buenas prácticas |
| `docs(tests): explicar estructura de carpetas de test` | Organización |
| `docs(license): actualizar tipo de licencia` | Legal |
| `docs(ui): agregar mockups o screenshots del sistema` | Visuales |

---

## 🛠 `chore`: Tareas de mantenimiento

| Ejemplo | Descripción |
|--------|-------------|
| `chore: agregar archivo .gitignore` | Setup |
| `chore: actualizar dependencias del proyecto` | Seguridad |
| `chore: limpiar carpetas no usadas` | Limpieza |
| `chore(env): configurar archivo .env.example` | Guía |
| `chore(ci): agregar GitHub Actions para testeo` | Automatización |
| `chore(lint): configurar ESLint y Prettier` | Estilo |
| `chore(repo): mover README al directorio raíz` | Organización |
| `chore(config): separar configuraciones por entorno` | Escalabilidad |
| `chore(package): actualizar versión de Node.js` | Compatibilidad |
| `chore(vscode): configurar ajustes de editor` | Dev |
| `chore(docker): agregar Dockerfile para entorno local` | Contenedores |

---

## ⚙️ `build` y `ci`: DevOps y despliegue

### `build`

```bash
build: configurar Webpack para producción
build(docker): agregar Dockerfile con Alpine
build: incluir script de build en package.json
build(env): cargar variables por entorno
build(vite): configurar alias y rutas absolutas
build: habilitar cache en build local
build: mover build a carpeta /dist
build(vercel): preparar archivo vercel.json
build(eslint): integrar revisión en proceso de build
build: configurar Babel para TS
build(api): minificar respuestas en producción
```

### `ci`

```bash
ci: configurar despliegue automático a Vercel
ci(github): agregar workflow para test en push
ci: integrar SonarCloud en pipeline
ci: agregar notificaciones de fallos en Slack
ci: configurar caching de dependencias
ci: dividir workflows por entorno
ci: verificar versión de Node en CI
ci: habilitar estrategia matrix para versiones
ci: actualizar job de deploy a staging
ci: agregar cobertura de código al pipeline
```

---

## 🏁 `chore`: Commit inicial

```bash
chore: commit inicial del proyecto con estructura base
```

