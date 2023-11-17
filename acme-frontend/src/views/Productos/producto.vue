<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Productos" sub-title="Productos de la empresa">
            <template #extra>
                <a-button type="primary" @click="showModal">Agregar</a-button>
                <a-modal v-model:open="open" title="Nuevo Producto" :confirm-loading="confirmLoading" @ok="handleOk">
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
                <a-modal v-model:open="openEditar" title="Editar Categoria" :confirm-loading="confirmLoading"
                    @ok="handleEditOk">
                    <a-space direction="vertical" style="width: 100%;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <a-select ref="select" v-model:value="value" style="width: 100%" :options="options"
                            placeholder="Categoria Asociada" />
                    </a-space>
                </a-modal>
            </template>
        </a-page-header>

        <div>
            <div class="w-full flex justify-center" v-if="data.length == 0">
                <a-spin :indicator="indicator" @spinning="true" />
            </div>
            <div v-else>
                <a-table :columns="columns" :data-source="data">
                    <template #headerCell="{ column }">
                        <template v-if="column.key === 'name'">
                            <span>
                                Name
                            </span>
                        </template>
                    </template>

                    <template #bodyCell="{ column, record }">
                        <template v-if="column.key === 'action'">
                            <span>
                                <a-button type="link" @click="editCategory(record)">Editar</a-button>
                                <a-divider type="vertical" />
                                <a-space>
                                    <a-button type="link" @click="showPromiseConfirm(record)" danger>Eliminar</a-button>
                                </a-space>
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
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { createVNode } from 'vue';
import { Modal } from 'ant-design-vue';
import { message } from 'ant-design-vue';
import ArchivosController from '../../services/ArchivosController'

const open = ref(false);
const openEditar = ref(false);
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

const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});

const columns = [
    {
        title: 'Foto',
        dataIndex: 'foto',
        sorter: true,
        width: '11%',
    },
    {
        title: 'Titulo',
        dataIndex: 'titulo',
        width: '11%',
    },
    {
        title: 'Descripcion',
        dataIndex: 'descripcion',
        width: '11%',
    },
    {
        title: 'Precio',
        dataIndex: 'precio',
        width: '11%',
    },
    {
        title: 'Tipo IVA',
        dataIndex: 'iva',
        width: '11%',
    },
    {
        title: 'PDF',
        dataIndex: 'pdf',
        width: '11%',
    },
    {
        title: 'Categoria',
        dataIndex: 'categoria',
        width: '11%',
    },
    {
        title: 'Opiniones',
        dataIndex: 'opiniones',
        width: '11%',
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
            })
            .catch((error) => {
                confirmLoading.value = false
                console.error('Error al obtener la lista de categorias:', error);
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
            console.error('Error al subir la imagen:', error);
            throw error; // Lanzar el error para que sea capturado por el bloque catch en handleOk
        });
};

const subirPdf = async () => {
    const formDataPdf = new FormData();
    formDataPdf.append('archivo', fileListPdf.value[0].originFileObj);

    return ArchivosController.cargarPdf(formDataPdf)
        .then((response) => response.data)
        .catch((error) => {
            console.error('Error al subir el PDF:', error);
            throw error; // Lanzar el error para que sea capturado por el bloque catch en handleOk
        });
};




const handleChangePdf = info => {
    if (info.file.status !== 'uploading') {
        console.log(info.file, info.fileList);
    }
    if (info.file.status === 'done') {
        message.success(`${info.file.name} file uploaded successfully`);
    } else if (info.file.status === 'error') {
        message.error(`${info.file.name} file upload failed.`);
    }
};
const fileListPdf = ref([]);
const headersPdf = {
    authorization: 'authorization-text',
};



const cargarCategorias = () => {
    const dataCategorias = CategoriaController.getCategorias()
        .then((response) => {

            options.value = response.data
                .map((categoria) => ({
                    value: categoria.id,
                    label: categoria.nombre,
                }))
                .sort((a, b) => a.label.localeCompare(b.label));
        })
        .catch((error) => {
            console.error('Error al obtener la lista de categorias:', error);
        });
}

const showModal = () => {
    nombre.value = '';
    value.value = null;
    open.value = true;
};


const handleEditOk = () => {

};


// Puedes implementar una función similar para editar si es necesario
const editCategory = (category) => {


};

function showPromiseConfirm(categoria) {
    console.log(categoria)
    Modal.confirm({
        title: '¿Deseas eliminar esta categoria?',
        icon: createVNode(ExclamationCircleOutlined),
        content: 'No se eliminara si tiene productos asociados',
        async onOk() {
            try {
                await CategoriaController.deleteCategorias(categoria.key)
                    .then(() => {
                        cargarCategorias()
                    })
                    .catch((error) => {
                        console.error(error);
                    });
                message.success('Categoría eliminada exitosamente');

            } catch {
                return console.log('Oops errors!');
            }
            cargarCategorias()
        },
        onCancel() { },
    });
}

onMounted(() => {
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