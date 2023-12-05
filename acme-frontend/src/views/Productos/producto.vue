<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Productos" sub-title="Productos de la empresa">
            <template #extra>
                <a-button type="primary" @click="showModal">Agregar</a-button>
                <a-modal v-model:open="open" title="Nuevo Producto" :footer="false" :afterClose="onModalClosed">
                    <a-form :model="formModel" name="basic" :label-col="{ span: 6 }" :wrapper-col="{ span: 16 }"
                        style="margin-top: 10%;" autocomplete="off" @finish="handleOk">
                        <a-form-item label="Foto" :rules="[{ required: true, message: 'La foto es obligatoria' }]">
                            <a-upload v-model:file-list="fileList" :before-upload="beforeUpload">
                                <a-button>Seleccionar Imagen</a-button>
                            </a-upload>
                        </a-form-item>

                        <a-form-item label="Titulo" name="titleRef"
                            :rules="[{ required: true, message: 'El título es obligatorio' }]">
                            <a-input v-model:value="formModel.titleRef" placeholder="Titulo" />
                        </a-form-item>

                        <a-form-item label="Descripcion" name="descriptionRef"
                            :rules="[{ required: true, message: 'La descripción es obligatoria' }]">
                            <a-textarea v-model:value="formModel.descriptionRef" :rows="4" placeholder="Descripcion" />
                        </a-form-item>

                        <a-form-item label="Categoria" name="value"
                            :rules="[{ required: true, message: 'La categoría es obligatoria' }]">
                            <a-select v-model:value="formModel.value" style="width: 100%" :options="options"
                                placeholder="Categoria" />
                        </a-form-item>

                        <a-form-item label="Precio" name="priceRef"
                            :rules="[{ required: true, message: 'El precio es obligatorio' }]">
                            <a-input-number v-model:value="formModel.priceRef" placeholder="Precio" />
                        </a-form-item>

                        <a-form-item label="IVA" name="IVA" :rules="[{ required: true, message: 'El IVA es obligatorio' }]">
                            <a-select v-model:value="formModel.IVA" style="width: 100%" :options="optionsIVA"
                                placeholder="IVA" />
                        </a-form-item>

                        <a-form-item label="PDF" :rules="[{ required: true, message: 'El pdf es obligatorio' }]">
                            <a-upload v-model:file-list="fileListPdf" name="file" :before-upload="handleChangePdf">
                                <a-button>Seleccionar PDF</a-button>
                            </a-upload>
                        </a-form-item>

                        <a-form-item :wrapper-col="{ offset: 20, span: 16 }">
                            <a-button type="primary" html-type="submit" :loading="confirmLoading">OK</a-button>
                        </a-form-item>
                    </a-form>
                </a-modal>
                <a-modal v-model:open="openEditar" title="Editar Producto" :footer="false" :afterClose="onModalClosed"
                    @ok="handleEditOk">
                    <a-form :model="formModel" name="basic" :label-col="{ span: 6 }" :wrapper-col="{ span: 16 }"
                        style="margin-top: 10%;" autocomplete="off" @finish="handleEditOk">
                        <a-form-item label="Foto" :rules="[{ required: true, message: 'La foto es obligatoria' }]">
                            <a-upload v-model:file-list="fileList" :before-upload="beforeUpload">
                                <a-button>Seleccionar Imagen</a-button>
                            </a-upload>
                        </a-form-item>

                        <a-form-item label="Titulo" name="titleRef"
                            :rules="[{ required: true, message: 'El título es obligatorio' }]">
                            <a-input v-model:value="formModel.titleRef" placeholder="Titulo" />
                        </a-form-item>

                        <a-form-item label="Descripcion" name="descriptionRef"
                            :rules="[{ required: true, message: 'La descripción es obligatoria' }]">
                            <a-textarea v-model:value="formModel.descriptionRef" :rows="4" placeholder="Descripcion" />
                        </a-form-item>

                        <a-form-item label="Categoria" name="value"
                            :rules="[{ required: true, message: 'La categoría es obligatoria' }]">
                            <a-select v-model:value="formModel.value" style="width: 100%" :options="options"
                                placeholder="Categoria" />
                        </a-form-item>

                        <a-form-item label="Precio" name="priceRef"
                            :rules="[{ required: true, message: 'El precio es obligatorio' }]">
                            <a-input-number v-model:value="formModel.priceRef" placeholder="Precio" />
                        </a-form-item>

                        <a-form-item label="IVA" name="IVA" :rules="[{ required: true, message: 'El IVA es obligatorio' }]">
                            <a-select v-model:value="formModel.IVA" style="width: 100%" :options="optionsIVA"
                                placeholder="IVA" />
                        </a-form-item>

                        <a-form-item label="PDF" :rules="[{ required: true, message: 'El pdf es obligatorio' }]">
                            <a-upload v-model:file-list="fileListPdf" name="file" :before-upload="handleChangePdf">
                                <a-button>Seleccionar PDF</a-button>
                            </a-upload>
                        </a-form-item>

                        <a-form-item :wrapper-col="{ offset: 20, span: 16 }">
                            <a-button type="primary" html-type="submit" :loading="confirmLoading">OK</a-button>
                        </a-form-item>
                    </a-form>
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
import { ref, h, onMounted, reactive } from 'vue';
import CategoriaController from '../../services/CategoriaController'
import ProductoController from '../../services/ProductosController'
import EmpresasController from '../../services/EmpresasController';
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { createVNode } from 'vue';
import { List, Modal, message } from 'ant-design-vue';
import ArchivosController from '../../services/ArchivosController'


const empresaId = ref(sessionStorage.getItem('empresaId'))

const formModel = reactive({
    titleRef: '',
    descriptionRef: '',
    value: '',
    priceRef: null,
    IVA: '',
});

const fileList = ref([]);
const fileListPdf = ref([]);

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
const data = ref([]);
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





const beforeUpload = (file) => {
    fileList.value = [file];
    return false; // Evita la carga automática de archivos
};

const handleOk = async () => {

    if (fileList.value.length == 0){
        message.info('Ingrese una imagen')
        return
    }

    if (fileListPdf.value.length == 0){
        message.info('Ingrese un pdf')
        return
    }

    confirmLoading.value = true
    try {
        const imageUrl = await subirImagen();
        const pdfUrl = await subirPdf();

        const data = {
            id: 0,
            titulo: formModel.titleRef,
            descripcion: formModel.descriptionRef,
            foto: imageUrl.ruta,
            precio: formModel.priceRef,
            tipo_iva: formModel.IVA,
            pdf: pdfUrl.ruta,
            empresaId: empresaId.value,
            categoriaId: formModel.value
        }

        ProductoController.createProducto(data)
            .then(() => {
                open.value = false;
                confirmLoading.value = false
                message.success('Producto creado')
                cargarProductos()
            })
            .catch((error) => {
                confirmLoading.value = false
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
    if (info.file.status === 'done') {
        message.success(`${info.file.name} Archivo subido exitosamente`);
    } else if (info.file.status === 'error') {
        message.error(`${info.file.name} Error al subir.`);
    }
};


const cargarCategorias = () => {
    CategoriaController.getCategorias(empresaId.value)
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
    EmpresasController.getById(empresaId.value)
        .then((response) => {
            data.value = response.data.productos.reverse()
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            message.error('Error al obtener la lista de productos');
        });
}

const showModal = () => {
    open.value = true;
};


const handleEditOk = async() => {
    const imageUrl = ref('')
    const pdfUrl = ref('')

    if(fileList.value.length == 0 ){
        imageUrl.value = editarProducto.value.foto
    } else {
        imageUrl.value = await subirImagen();
    }

    if(fileListPdf.value.length == 0 ){
        pdfUrl.value = editarProducto.value.pdf
    } else {
        pdfUrl.value = await subirPdf();
    }

    const data = {
        id: editarProducto.value.id,
        titulo: formModel.titleRef,
        descripcion: formModel.descriptionRef,
        foto: imageUrl.value.ruta,
        precio: formModel.priceRef,
        tipo_iva: formModel.IVA,
        pdf: pdfUrl.value.ruta,
        empresaId: empresaId.value,
        categoriaId: formModel.value
    }

    confirmLoading.value = true;
    ProductoController.editarProducto(data.id, data)
        .then(() => {
            openEditar.value = false;
            confirmLoading.value = false;
            fileList.length = 0;
            fileListPdf.length = 0;
            message.success('Producto editado');
            cargarProductos();
        })
        .catch((error) => {
            message.error('Error al editar el producto');
        });
};


// Puedes implementar una función similar para editar si es necesario
const editProducto = (producto) => {

    editarProducto.value = producto,
        formModel.titleRef = producto.titulo,
        formModel.descriptionRef = producto.descripcion,
        formModel.value = producto.categoriaId,
        formModel.priceRef = producto.precio,
        formModel.IVA = producto.tipo_iva

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
    ProductoController.getProductoById(data.id)
        .then((response) => {
            opiniones.value = response.data.opinionesAsociadas;
            openOpiniones.value = true;
        })
        .catch((error) => {
            message.error('Error al cargar las opiniones');
        });

};

const onModalClosed = () => {
    formModel.titleRef = '',
    formModel.descriptionRef = '',
    formModel.value = ''
    formModel.priceRef = null,
    formModel.IVA = ''
    fileList.value = []
    fileListPdf.value = []
};


function showPromiseConfirm(producto) {
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
}
</style>../../services/ArchivosController