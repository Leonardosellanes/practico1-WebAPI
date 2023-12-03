<template>
    <div class=" w-full h-full space-y-10">
        <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Empleados">
            <template #extra>
                <a-button type="primary" @click="selected = 0; open = true;">Agregar</a-button>
                <a-modal v-model:open="open" title="Agregar Empleado" :confirm-loading="confirmLoading" :footer="false">
                    <a-form :model="formState" name="basic" :label-col="{ span: 6 }" :wrapper-col="{ span: 16 }"
                        style="margin-top: 10%;" autocomplete="off" @finish="onFinish">
                        <a-form-item label="Nombre" name="name" :rules="[{ required: true, message: 'Ingrese el nombre' }]">
                            <a-input v-model:value="formState.name" />
                        </a-form-item>

                        <a-form-item label="Apellido" name="lName"
                            :rules="[{ required: true, message: 'Ingrese el apellido' }]">
                            <a-input v-model:value="formState.lName" />
                        </a-form-item>

                        <a-form-item label="Email" name="email"
                            :rules="[{ required: true, message: 'Ingrese un correo valido', type: 'email' }]">
                            <a-input v-model:value="formState.email" />
                        </a-form-item>

                        <!--a-form-item label="Direccion" name="address"
                            :rules="[{ required: true, message: 'Ingrese una direccion' }]">
                            <a-input v-model:value="formState.address" />
                        </a-form-item -->

                        <a-form-item label="Contraseña" name="password"
                            :rules="[{ required: true, message: 'Ingrese una contraseña' }]">
                            <a-input-password v-model:value="formState.password" />
                        </a-form-item>

                        <!--a-form-item name="isAdmin" :wrapper-col="{ offset: 6, span: 16 }">
                            <a-checkbox v-model:checked="formState.isAdmin">Admin</a-checkbox>
                        </a-form-item -->

                        <a-form-item :wrapper-col="{ offset: 20, span: 16 }">
                            <a-button type="primary" html-type="submit" :loading="confirmLoading">OK</a-button>
                        </a-form-item>
                    </a-form>
                </a-modal>
            </template>
        </a-page-header>
        <a-table :columns="columns" :data-source="data.value" class="w-full px-6" v-if="loading == false">
            <template #bodyCell="{ column, record }">
                <!-- template v-if="column.key === 'isAdmin'">
                    <span v-if="record.isAdmin == false">No</span>
                    <span v-else>Si</span>
                </template -->
                <template v-if="column.key === 'action'">
                    <span>
                        <a @click="modalEdit(record)">Editar</a>
                        <a-divider type="vertical" />
                        <a @click="modalDelete(record.id)">Eliminar</a>
                        
                    </span>
                </template>
            </template>
        </a-table>
    </div>
</template>

<script setup>
import { ref, onMounted, reactive } from 'vue';
import EmpresasController from '../../services/EmpresasController';
import { createVNode } from 'vue';
import { Modal, message } from 'ant-design-vue';
import { LoadingOutlined, ExclamationCircleOutlined } from '@ant-design/icons-vue';

const empresaId = ref(sessionStorage.getItem('empresaId'));

const loading = ref(false);
const confirmLoading = ref(false);
const open = ref(false);
const data = [];
const selected = ref(0);
const formState = reactive({
    name: '',
    lName: '',
    email: '',
    address: '',
    password: '',
    isAdmin: false,
    empresaId: empresaId.value
});

const columns = [
    {
        title: 'Nombre',
        dataIndex: 'name',
        width: '10%'
    },
    {
        title: 'Apellido',
        dataIndex: 'lName',
        width: '10%'
    },
    {
        title: 'Email',
        dataIndex: 'email',
        width: '10%'
    },
    /*{
        title: 'Direccion',
        dataIndex: 'address',
        width: '10%'
    },*/
    /*{
        title: 'Administrador',
        key: 'isAdmin',
        width: '10%'
    },*/
    {
        title: '',
        key: 'action',
        width: '15%'
    },
];

const modalEdit = (record) => {
    formState.name = record.name;
    formState.lName = record.lName;
    formState.email = record.email;
    formState.password = record.password;
    selected.value = record.id;
    open.value = true;
}

const cargarEmpleados = () => {
    loading.value = true
    EmpresasController.getEmpleados(empresaId.value)
        .then((response) => {
            data.value = response.data.reverse()
            loading.value = false;
        })
        .catch((error) => {
            loading.value = false;
            console.error('Error al obtener la lista de empleados:', error);
        });
}

const modalDelete = (id) => {
    Modal.confirm({
        title: '¿Deseas eliminar este empleado?',
        icon: createVNode(ExclamationCircleOutlined),
        async onOk() {
            try {
                await EmpresasController.deleteEmpleado(id)
                    .then(() => {
                        cargarEmpleados();
                    })
                    .catch((error) => {
                        console.error(error);
                    });
                message.success('Empleado eliminado exitosamente');

            } catch {
                message.error('Ha ocurrido un error, intente nuevamente')
            }
            cargarEmpleados();
        },
        onCancel() { },
    });
}

const onFinish = values => {
    confirmLoading.value = true;
    if(selected.value == 0){
    EmpresasController.createEmpleados(formState)
        .then(() => {
            open.value = false;
            confirmLoading.value = false;
            cargarEmpleados();
        })
        .catch((error) => {
            confirmLoading.value = false;
            console.error('Error al obtener la lista de empleados:', error);
        });
    }else{
        EmpresasController.updateEmpleado(selected.value, formState.email, formState.password, formState.name, formState.lName)
        .then(() => {
            open.value = false;
            confirmLoading.value = false;
            cargarEmpleados();
        })
        .catch((error) => {
            confirmLoading.value = false;
            console.error('Error al obtener la lista de empleados:', error);
        });
    }

};

const deleteEmpleado = 

onMounted(() => {
    cargarEmpleados();
});


</script>