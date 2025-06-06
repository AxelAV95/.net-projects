// Funciones para gestión dinámica de tareas
class TareaManager {
    constructor() {
        this.initializeEventListeners();
        this.initializeAnimations();
    }

    initializeEventListeners() {
        // Manejar cambios de estado con AJAX
        document.addEventListener('click', (e) => {
            if (e.target.closest('.btn-estado-tarea')) {
                e.preventDefault();
                this.cambiarEstadoTarea(e.target.closest('.btn-estado-tarea'));
            }
        });

        // Manejar filtros con animaciones
        document.addEventListener('click', (e) => {
            if (e.target.closest('.filtro-tarea')) {
                e.preventDefault();
                this.aplicarFiltro(e.target.closest('.filtro-tarea'));
            }
        });
    }

    initializeAnimations() {
        // Animar entrada de tareas
        const tareas = document.querySelectorAll('.tarea-item');
        tareas.forEach((tarea, index) => {
            tarea.style.animationDelay = `${index * 0.1}s`;
            tarea.classList.add('fade-in');
        });
    }

    async cambiarEstadoTarea(button) {
        const tareaId = button.dataset.tareaId;
        const tareaElement = button.closest('.list-group-item');
        
        // Determinar estado actual CORRECTAMENTE
        const estaCompletadaActualmente = button.innerHTML.includes('✅');
        const nuevoEstado = !estaCompletadaActualmente; // Invertir el estado
        
        console.log('Estado actual:', estaCompletadaActualmente ? 'Completada' : 'Pendiente');
        console.log('Nuevo estado:', nuevoEstado ? 'Completada' : 'Pendiente');
        
        // Mostrar indicador de carga
        const originalContent = button.innerHTML;
        button.innerHTML = '<span class="spinner-border spinner-border-sm" role="status"></span>';
        button.disabled = true;

        try {
            // Obtener token antiforgery
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
            
            const formData = new FormData();
            formData.append('id', tareaId);
            formData.append('__RequestVerificationToken', token);

            const response = await fetch('/Tareas/Index?handler=CambiarEstado', {
                method: 'POST',
                body: formData
            });

            if (response.ok) {
                // Animar cambio de estado
                tareaElement.classList.add('updating');
                
                setTimeout(() => {
                    if (nuevoEstado) {
                        // Cambiar a completada
                        button.innerHTML = '✅';
                        button.classList.remove('btn-outline-secondary');
                        button.classList.add('btn-success');
                        button.title = 'Marcar como pendiente';
                        
                        tareaElement.classList.add('list-group-item-success');
                        const titulo = tareaElement.querySelector('.tarea-titulo');
                        const descripcion = tareaElement.querySelector('.tarea-descripcion');
                        
                        if (titulo) {
                            titulo.classList.add('text-decoration-line-through', 'text-muted');
                        }
                        if (descripcion) {
                            descripcion.classList.add('text-muted');
                        }
                        
                        // Mostrar mensaje de éxito
                        this.mostrarNotificacion('¡Tarea completada! 🎉', 'success');
                        
                    } else {
                        // Cambiar a pendiente
                        button.innerHTML = '⬜';
                        button.classList.remove('btn-success');
                        button.classList.add('btn-outline-secondary');
                        button.title = 'Marcar como completada';
                        
                        tareaElement.classList.remove('list-group-item-success');
                        const titulo = tareaElement.querySelector('.tarea-titulo');
                        const descripcion = tareaElement.querySelector('.tarea-descripcion');
                        
                        if (titulo) {
                            titulo.classList.remove('text-decoration-line-through', 'text-muted');
                        }
                        if (descripcion) {
                            descripcion.classList.remove('text-muted');
                        }
                        
                        // Mostrar mensaje de cambio
                        this.mostrarNotificacion('Tarea marcada como pendiente', 'info');
                    }
                    
                    tareaElement.classList.remove('updating');
                    button.disabled = false;
                    
                    // Actualizar estadísticas
                    this.actualizarEstadisticas();
                    
                }, 300);
                
            } else {
                throw new Error(`Error HTTP: ${response.status}`);
            }
            
        } catch (error) {
            console.error('Error al cambiar estado:', error);
            button.innerHTML = originalContent;
            button.disabled = false;
            this.mostrarNotificacion('Error al cambiar el estado de la tarea', 'error');
        }
    }

    // ... resto de métodos sin cambios
    aplicarFiltro(filtroButton) {
        const filtro = filtroButton.dataset.filtro;
        const tareas = document.querySelectorAll('.tarea-item');
        
        // Actualizar botones de filtro
        document.querySelectorAll('.filtro-tarea').forEach(btn => {
            btn.classList.remove('btn-primary', 'btn-warning', 'btn-success');
            btn.classList.add('btn-outline-primary', 'btn-outline-warning', 'btn-outline-success');
        });
        
        filtroButton.classList.remove('btn-outline-primary', 'btn-outline-warning', 'btn-outline-success');
        if (filtro === 'pendientes') {
            filtroButton.classList.add('btn-warning');
        } else if (filtro === 'completadas') {
            filtroButton.classList.add('btn-success');
        } else {
            filtroButton.classList.add('btn-primary');
        }

        // Filtrar tareas con animación
        tareas.forEach((tarea, index) => {
            const esCompletada = tarea.classList.contains('list-group-item-success');
            let mostrar = false;

            switch (filtro) {
                case 'completadas':
                    mostrar = esCompletada;
                    break;
                case 'pendientes':
                    mostrar = !esCompletada;
                    break;
                default: // 'todas'
                    mostrar = true;
                    break;
            }

            if (mostrar) {
                tarea.style.display = 'block';
                tarea.style.animationDelay = `${index * 0.05}s`;
                tarea.classList.add('fade-in');
            } else {
                tarea.classList.add('fade-out');
                setTimeout(() => {
                    tarea.style.display = 'none';
                }, 200);
            }
        });
    }

    actualizarEstadisticas() {
        const tareas = document.querySelectorAll('.tarea-item[style*="display: block"], .tarea-item:not([style*="display: none"])');
        const tareasCompletadas = document.querySelectorAll('.tarea-item.list-group-item-success[style*="display: block"], .tarea-item.list-group-item-success:not([style*="display: none"])');
        const tareasPendientes = tareas.length - tareasCompletadas.length;

        console.log('Actualizando estadísticas:', {
            total: tareas.length,
            completadas: tareasCompletadas.length,
            pendientes: tareasPendientes
        });

        // Actualizar contadores con animación
        this.animarContador('.total-tareas', tareas.length);
        this.animarContador('.completadas-count', tareasCompletadas.length);
        this.animarContador('.pendientes-count', tareasPendientes);
    }

    animarContador(selector, valorFinal) {
        const elemento = document.querySelector(selector);
        if (!elemento) return;

        const valorInicial = parseInt(elemento.textContent) || 0;
        const duracion = 300;
        const incremento = (valorFinal - valorInicial) / (duracion / 16);

        let valorActual = valorInicial;
        const intervalo = setInterval(() => {
            valorActual += incremento;
            if ((incremento > 0 && valorActual >= valorFinal) || 
                (incremento < 0 && valorActual <= valorFinal)) {
                elemento.textContent = valorFinal;
                clearInterval(intervalo);
            } else {
                elemento.textContent = Math.round(valorActual);
            }
        }, 16);
    }

    mostrarNotificacion(mensaje, tipo = 'info') {
        // Crear notificación toast
        const toast = document.createElement('div');
        toast.className = `toast align-items-center text-white bg-${tipo === 'error' ? 'danger' : tipo === 'success' ? 'success' : 'info'} border-0`;
        toast.setAttribute('role', 'alert');
        toast.setAttribute('aria-live', 'assertive');
        toast.setAttribute('aria-atomic', 'true');
        
        toast.innerHTML = `
            <div class="d-flex">
                <div class="toast-body">
                    ${mensaje}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        `;

        // Añadir al contenedor de toasts
        let toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            toastContainer = document.createElement('div');
            toastContainer.id = 'toast-container';
            toastContainer.className = 'toast-container position-fixed bottom-0 end-0 p-3';
            document.body.appendChild(toastContainer);
        }

        toastContainer.appendChild(toast);
        
        // Mostrar toast
        const bsToast = new bootstrap.Toast(toast);
        bsToast.show();

        // Remover del DOM cuando se oculte
        toast.addEventListener('hidden.bs.toast', () => {
            toast.remove();
        });
    }
}

// Inicializar cuando se carga la página
document.addEventListener('DOMContentLoaded', () => {
    new TareaManager();
});