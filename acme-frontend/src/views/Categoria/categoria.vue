<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Categorias" sub-title="Categorias de la empresa">
            <template #extra>
                <a-button type="primary" @click="showModal">Agregar</a-button>
                <a-modal v-model:open="open" title="Agregar Categoria" :confirm-loading="confirmLoading" @ok="handleOk">
                    <a-space direction="vertical" style="width: 100%;">
                        <a-input v-model:value="nombre" placeholder="Nombre" />
                        <a-select ref="select" v-model:value="value" style="width: 100%" :options="options"
                            placeholder="Categoria Asociada" />
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
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { createVNode } from 'vue';
import { Modal } from 'ant-design-vue';

const open = ref(false);
const openEditar = ref(false);
const confirmLoading = ref(false);
const nombre = ref('');
const options = ref([]);
const value = ref('');
const data = ref([]);
const editarCategoria = ref([]);

const indicator = h(LoadingOutlined, {
    style: {
        fontSize: '24px',
    },
    spin: true,
});

const columns = [
    {
        title: 'Nombre',
        dataIndex: 'name',
        sorter: true,
        width: '33%',
    },
    {
        title: 'Categoria asociada',
        dataIndex: 'asociated',
        width: '33%',
    },
    {
        title: 'Acciones',
        key: 'action',
    },
];

const cargarCategorias = () => {
    const dataCategorias = CategoriaController.getCategorias()
        .then((response) => {
            data.value = response.data
                .map((categoria) => ({
                    key: categoria.id,
                    name: categoria.nombre,
                    asociated: categoria.categoriaAsociada ? categoria.categoriaAsociada.nombre : "",

                }))
                .sort((a, b) => b.key - a.key);


            // Formatear para 'options'
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

const handleOk = () => {
    const data = {
        Id: 0,
        nombre: nombre.value,
        categoriaId: value.value,
        empresaId: 1
    }

    confirmLoading.value = true;
    const create = CategoriaController.createCategorias(data)
        .then(() => {
            open.value = false;
            confirmLoading.value = false;
            cargarCategorias()
        })
        .catch((error) => {
            console.error('Error al crear categoria:', error);
        });
};

const handleEditOk = () => {

    const foundCategory = options.value.find((opt) => opt.label === value.value);
    const key = foundCategory ? foundCategory.key : null;

    const data = {
        id: editarCategoria.value.key,
        nombre: nombre.value,
        categoriaId: key,
        empresaId: 1
    }

    console.log(data)
    confirmLoading.value = true;
    const create = CategoriaController.editarCategorias(data.id, data)
        .then(() => {
            openEditar.value = false;
            confirmLoading.value = false;
            cargarCategorias()
        })
        .catch((error) => {
            console.error('Error al editar categoria:', error);
        });
};


// Puedes implementar una función similar para editar si es necesario
const editCategory = (category) => {


    editarCategoria.value = category
    nombre.value = category.name
    value.value = category.asociated
    openEditar.value = true

    // Aquí puedes realizar la lógica para editar la categoría
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