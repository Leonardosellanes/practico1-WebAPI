<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Productos" sub-title="Productos de la empresa">
            <template #extra>
                <a-button type="primary" @click="showModal">Agregar</a-button>
                <a-modal v-model:open="open" title="Nuevo Producto" :confirm-loading="confirmLoading" @ok="handleOk">
                    <a-space direction="vertical" style="width: 100%;">
                        Foto:
                        <a-upload v-model:file-list="fileList" :before-upload="beforeUpload" :status="errorFoto">
                            <a-button>Seleccionar Imagen</a-button>
                        </a-upload>

                        <a-input v-model:value="titleRef" placeholder="Titulo" :status="errorTitulo" />

                        <a-textarea v-model:value="descriptionRef" :rows="4" placeholder="Descripcion"
                            :status="errorDescripcion" />

                        <a-select ref="select" v-model:value="value" style="width: 100%" :options="options"
                            :status="errorCategoria" placeholder="Categoria" />

                        <a-input-number v-model:value="priceRef" placeholder="Precio" :status="errorPrecio" />

                        <a-select ref="select" v-model:value="IVA" style="width: 100%" :options="optionsIVA"
                            :status="errorIva" placeholder="IVA" />

                        PDF:
                        <a-upload v-model:file-list="fileListPdf" name="file" :before-upload="handleChangePdf"
                            :status="errorPdf">
                            <a-button>
                                Seleccionar PDF
                            </a-button>
                        </a-upload>

                    </a-space>
                </a-modal>
                <a-modal v-model:open="openEditar" title="Editar Categoria" :confirm-loading="confirmLoading"
                    @ok="handleEditOk">
                    <a-space direction="vertical" style="width: 100%;">
                        Foto:
                        <a-upload v-model:file-list="fileList" :before-upload="beforeUpload">
                            <a-button>Seleccionar Imagen</a-button>
                        </a-upload>

                        <a-input v-model:value="titleRef" placeholder="Titulo" />

                        <a-textarea v-model:value="descriptionRef" :rows="4" placeholder="Descripcion" />

                        <a-select ref="select" v-model:value="value" style="width: 100%" :options="options"
                            placeholder="Categoria" />

                        <a-input-number v-model:value="priceRef" placeholder="Precio" />

                        <a-select ref="select" v-model:value="IVA" style="width: 100%" :options="optionsIVA"
                            placeholder="IVA" />

                        PDF:
                        <a-upload v-model:file-list="fileListPdf" name="file" :before-upload="handleChangePdf">
                            <a-button>
                                Seleccionar PDF
                            </a-button>
                        </a-upload>

                    </a-space>
                </a-modal>

                <a-modal v-model:open="openOpiniones" title="Opiniones" :footer="null" :closable="true"
                    :bodyStyle="{ maxHeight: '500px', overflowY: 'auto' }">
                    <a-space direction="vertical" style="width: 100%;">
                        <a-list item-layout="vertical" size="large" :data-source="opiniones.slice().reverse()">
                            <template #renderItem="{ item }">
                                <a-list-item>
                                    <a-card :title="item.titulo">
                                        <a-space direction="vertical" style="width: 100%;">
                                            {{ item.descripcion }}
                                            <a-rate :value="item.estrellas" disabled />
                                        </a-space>
                                    </a-card>
                                </a-list-item>
                            </template>
                        </a-list>
                    </a-space>
                </a-modal>

            </template>
        </a-page-header>

        <div>
            <div class="w-full flex justify-center" v-if="loading">
                <a-spin :indicator="indicator" @spinning="true" />
            </div>
            <div v-else>
                <a-table :columns="columns" :data-source="data" customRender="customRender">
                    <template #bodyCell="{ column, record }">
                        <template v-if="column.key === 'action'">
                            <span>
                                <a-button type="link" @click="editProducto(record)">Editar</a-button>
                                <a-divider type="vertical" />
                                <a-space>
                                    <a-button type="link" @click="showPromiseConfirm(record)" danger>Eliminar</a-button>
                                </a-space>
                            </span>
                        </template>
                        <template v-if="column.key === 'pdf'">
                            <span>
                                <a-button type="link" @click="viewPdf(record)">Ver</a-button>
                            </span>
                        </template>
                        <template v-if="column.key === 'opiniones'">
                            <span>
                                <a-button type="link" @click="viewOpiniones(record)">Ver</a-button>
                            </span>
                        </template>
                    </template>
                </a-table>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, h, onMounted } from 'vue';
import CategoriaController from '../../services/CategoriaController'
import ProductoController from '../../services/ProductosController'
import EmpresasController from '../../services/EmpresasController';
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { createVNode } from 'vue';
import { Modal, message } from 'ant-design-vue';
import ArchivosController from '../../services/ArchivosController'

const loading = ref(false)
const open = ref(false);
const openEditar = ref(false);
const openOpiniones = ref(false);
const confirmLoading = ref(false);
const nombre = ref('');
const options = ref([]);
const optionsIVA = ref([
    {
        value: 'Exonerado',
        label: 'Exonerado',
    },
    {
        value: 'Reducido',
        label: 'Reducido',
    },
    {
        value: 'General',
        label: 'General',
    },]);
const value = ref('');
const IVA = ref('');
const data = ref([]);
const titleRef = ref('');
const descriptionRef = ref('');
const priceRef = ref(null);
const editarProducto = ref([]);
const opiniones = ref([])
const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});

const columns = [
    {
        title: 'Titulo',
        dataIndex: 'titulo',
        width: '14%',
    },
    {
        title: 'Descripcion',
        dataIndex: 'descripcion',
        width: '14%',
    },
    {
        title: 'Precio',
        dataIndex: 'precio',
        width: '14%',
    },
    {
        title: 'Tipo IVA',
        dataIndex: 'tipo_iva',
        width: '14%',
    },
    {
        title: 'PDF',
        width: '14%',
        key: 'pdf'
    },
    {
        title: 'Opiniones',
        width: '14%',
        key: 'opiniones'
    },
    {
        title: 'Acciones',
        key: 'action',
    },
];

const fileList = ref([]);
const headers = {
    authorization: 'authorization-text',
};

const errorFoto = ref('');
const errorTitulo = ref('');
const errorDescripcion = ref('');
const errorCategoria = ref('');
const errorIva = ref('');
const errorPrecio = ref('');
const errorPdf = ref('');

const beforeUpload = (file) => {
    fileList.value = [file];
    return false; // Evita la carga automática de archivos
};
const handleOk = async () => {

        confirmLoading.value = true
        try {
            const imageUrl = await subirImagen();
            const pdfUrl = await subirPdf();

            // Después de que ambas operaciones hayan finalizado con éxito
            console.log('Imagen subida:', imageUrl);
            console.log('PDF subido:', pdfUrl);

            const data = {
                id: 0,
                titulo: titleRef.value,
                descripcion: descriptionRef.value,
                foto: imageUrl.ruta,
                precio: priceRef.value,
                tipo_iva: IVA.value,
                pdf: pdfUrl.ruta,
                empresaId: 1,
                categoriaId: value.value
            }

            const producto = ProductoController.createProducto(data)
                .then(() => {
                    open.value = false;
                    confirmLoading.value = false
                    message.success('Producto creado')
                    cargarProductos()
                })
                .catch((error) => {
                    confirmLoading.value = false
                    console.error(':', error);
                    message.error('Error al crear el producto');
                });
        } catch (error) {

            console.error('Error en handleOk:', error);
        }
};

const subirImagen = async () => {
    const formData = new FormData();
    formData.append('archivo', fileList.value[0].originFileObj);

    return ArchivosController.cargarImagen(formData)
        .then((response) => response.data)
        .catch((error) => {
            message.error('Error al subir la imagen');
            throw error; // Lanzar el error para que sea capturado por el bloque catch en handleOk
        });
};

const subirPdf = async () => {
    const formDataPdf = new FormData();
    formDataPdf.append('archivo', fileListPdf.value[0].originFileObj);

    return ArchivosController.cargarPdf(formDataPdf)
        .then((response) => response.data)
        .catch((error) => {
            message.error('Error al subir el pdf');
            throw error; // Lanzar el error para que sea capturado por el bloque catch en handleOk
        });
};

const handleChangePdf = info => {
    if (info.file.status !== 'uploading') {
        console.log(info.file, info.fileList);
    }
    if (info.file.status === 'done') {
        message.success(`${info.file.name} Archivo subido exitosamente`);
    } else if (info.file.status === 'error') {
        message.error(`${info.file.name} Error al subir.`);
    }
};
const fileListPdf = ref();
const headersPdf = {
    authorization: 'authorization-text',
};

const cargarCategorias = () => {
    const dataCategorias = CategoriaController.getCategorias(1)
        .then((response) => {

            options.value = response.data
                .map((categoria) => ({
                    value: categoria.id,
                    label: categoria.nombre,
                }))
                .sort((a, b) => a.label.localeCompare(b.label));
        })
        .catch((error) => {
            message.error('Error al obtener la lista de productos');
        });
}

const cargarProductos = () => {
    loading.value = true
    const dataProductos = EmpresasController.getById(1)
        .then((response) => {
            data.value = response.data.productos
            loading.value= false;
        })
        .catch((error) => {
            loading.value= false;
            console.log(error)
            message.error('Error al obtener la lista de productos');
        });
}

const showModal = () => {
    nombre.value = '';
    value.value = null;
    open.value = true;
};


const handleEditOk = () => {
    const foundCategory = options.value.find((opt) => opt.label === value.value);
    const key = foundCategory ? foundCategory.key : null;
    console.log(options.value)
    console.log(value.value)

    const data = {
        id: editarProducto.value.id,
        titulo: titleRef.value,
        descripcion: descriptionRef.value,
        foto: editarProducto.value.foto,
        precio: priceRef.value,
        tipo_iva: IVA.value,
        pdf: editarProducto.value.pdf,
        empresaId: 1,
        categoriaId: value.value
    }

    console.log(data)
    confirmLoading.value = true;
    const create = ProductoController.editarProducto(data.id, data)
        .then(() => {
            openEditar.value = false;
            confirmLoading.value = false;
            message.success('Producto editado');
            cargarProductos();
        })
        .catch((error) => {
            message.error('Error al editar el producto');
        });
};


// Puedes implementar una función similar para editar si es necesario
const editProducto = (producto) => {

    editarProducto.value = producto;
    titleRef.value = producto.titulo,
        descriptionRef.value = producto.descripcion,
        value.value = producto.categoriaId,
        priceRef.value = producto.precio,
        IVA.value = producto.tipo_iva

    openEditar.value = true;
};

const viewPdf = (data) => {
    if (data.base64pdf) {
        const binaryData = atob(data.base64pdf);
        const arrayBuffer = new ArrayBuffer(binaryData.length);
        const uint8Array = new Uint8Array(arrayBuffer);

        for (let i = 0; i < binaryData.length; i++) {
            uint8Array[i] = binaryData.charCodeAt(i);
        }

        const blob = new Blob([uint8Array], { type: 'application/pdf' });
        const blobUrl = URL.createObjectURL(blob);

        window.open(blobUrl, '_blank');
    } else {
        console.error('La cadena Base64 del PDF es nula o vacía.');
    }
};


const viewOpiniones = (data) => {
    console.log(data)
    const producto = ProductoController.getProductoById(data.id)
    .then((response) => {
           opiniones.value = response.data.opinionesAsociadas;
            openOpiniones.value = true;
        })
        .catch((error) => {
            message.error('Error al cargar las opiniones');
        });
    
};


function showPromiseConfirm(producto) {
    console.log(producto)
    Modal.confirm({
        title: '¿Deseas eliminar este producto?',
        icon: createVNode(ExclamationCircleOutlined),
        content: 'No se eliminara si tiene compras asociadas',
        async onOk() {
            try {
                await ProductoController.deleteProducto(producto.id)
                    .then(() => {
                        cargarCategorias()
                    })
                    .catch((error) => {
                        console.error(error);
                    });
                message.success('Producto eliminado exitosamente');

            } catch {
                return message.error('Error al eliminar el producto');
            }
            cargarProductos();
        },
        onCancel() { },
    });
}

onMounted(() => {
    cargarProductos()
    cargarCategorias()
});

</script>

<style scoped>
.avatar-uploader>.ant-upload {
    width: 128px;
    height: 128px;
}

.ant-upload-select-picture-card i {
    font-size: 32px;
    color: #999;
}

.ant-upload-select-picture-card .ant-upload-text {
    margin-top: 8px;
    color: #666;
}</style>../../services/ArchivosController